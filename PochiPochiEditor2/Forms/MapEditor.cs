using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Commands;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;
using PochiPochiEditor2.Utilities.Tokens;

namespace PochiPochiEditor2.Forms
{
    [FormGroup(FormGroup.Map, 1)]
    public partial class MapEditor : Form
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;
        // 変更履歴用
        private UndoManager _undoManager = null;
        // 各テーブル用
        private EntryManager _mapNameEntry = null;
        private List<Entry[]> _mapHeaderEntry = null;
        // 再利用する変数を保持
        private int _mapNameFirstIndex = default;
        private Dictionary<int, string> _mapNameCache = new Dictionary<int, string>();

        private enum FieldKey
        {
            MapNamePointerOffset,

            MapHeaderPointerOffset,
            MapHeaderOffset,

            MapFooterOffset,
            EventScriptHeaderOffset,
            LevelScriptOffset,
            ConnHeaderOffset,
            BgmIndex,
            MapTerrainIndex,
            MapNameIndex,
            MapSight,
            MapWthr,
            MapType,
            MapBike,
            MapNameType,
            MapRelLayer,
            MapSpBg
        }

        private static class DefName
        {
            public static string MapNamePointerEntry = nameof(MapNamePointerEntry);

            public static string MapBankPointerEntry = nameof(MapBankPointerEntry);
            public static string MapNumberPointerEntry = nameof(MapNumberPointerEntry);
            public static string MapHeaderEntry = nameof(MapHeaderEntry);
        }

        private static class IniKey
        {
            public static string MapNameTableOffset = nameof(MapNameTableOffset);
            public static string MapNameCount = nameof(MapNameCount);
            public static string MapNameFirstIndex = nameof(MapNameFirstIndex);

            public static string MapBankTableOffset = nameof(MapBankTableOffset);
        }

        public MapEditor(SharedData sharedData, UndoManager undoManager)
        {
            InitializeComponent();
            _sharedData = sharedData;
            _undoManager = undoManager;

            InitializeMapNameEntry(); // 先に処理
            InitializeControls();
            InitializeMapHeaderEntry();

            UpdateMapNameComboBox();
            UpdateMapSelector();


            // InitializeOtherEntries();

            // InitializePipelines();
            // InitializeEventHandlers();


        }

        private void InitializeMapNameEntry()
        {
            // マップ名テーブルを作成
            string defFileName = DefName.MapNamePointerEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.MapNameTableOffset);
            int entrycount = _sharedData.Config.ReadInt(IniKey.MapNameCount);
            _mapNameEntry = new EntryManager(
                defFileName,
                typeof(FieldKey),
                _sharedData,
                tableOffset,
                entrycount);
        }

        private void InitializeControls()
        {
            // 各コンボボックスにアイテムを追加
            CtrlHelper.LoadComboBoxFromFile(
                (cmbMapType, "txt/Map/MapType.txt"),
                (cmbMapWthr, "txt/Map/MapWthr.txt"),
                (cmbMapSight, "txt/Map/MapSight.txt"),
                (cmbMapBike, "txt/Map/MapBike.txt"),
                (cmbMapSpBg, "txt/Map/MapSpBg.txt"),
                (cmbMapNameType, "txt/Map/MapNameType.txt"));
        }

        private void InitializeMapHeaderEntry()
        {
            // マップヘッダーエントリー格納先を先に作成
            _mapHeaderEntry = new List<Entry[]>();

            string defFileName = DefName.MapBankPointerEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.MapBankTableOffset);
            // エントリー数を仮カウント（誤って含まれている可能性）
            var pointerPattern = new List<TokenData>()
            {
                TokenData.Pointer()
            };
            var bankEntrycount = PatternMatcher.TryCount(
                pointerPattern,
                _sharedData.RomData,
                tableOffset,
                allowNullPointer: false); // nullポインタを許容しない

            // マップバンクテーブルを仮作成
            var mapBankEntry = new EntryManager(
                defFileName,
                typeof(FieldKey),
                _sharedData, 
                tableOffset, 
                bankEntrycount);

            // マップナンバーテーブルの先頭オフセットをすべて取得
            var mapNumberTableOffsets = new List<int>();
            for (int i = 0; i < mapBankEntry.Entries.Count; i++)
            {
                var mapNumberTableOffset = 
                    mapBankEntry.Entries[i][FieldKey.MapHeaderPointerOffset].GetData<int>();

                // 正しいテーブルオフセットの検証は、ポインタ判定が限界
                var IsValid = PatternMatcher.TryMatch(
                    pointerPattern,
                    _sharedData.RomData,
                    mapNumberTableOffset,
                    allowNullPointer: true); // nullポインタを許容する

                // 無効なオフセットだったら、そこで中断
                if (!IsValid) break;

                mapNumberTableOffsets.Add(mapNumberTableOffset);
            }

            // 定数を事前に計算
            var entryLength = TokenData.Pointer().GetLength();
            var headerPattern = new List<TokenData>()
            {
                TokenData.Pointer(),
                TokenData.Pointer(),
                TokenData.Pointer(),
                TokenData.Pointer(),
                TokenData.Wildcard(4),
                TokenData.Range((byte)_mapNameFirstIndex, byte.MaxValue, Constants.ByteSize),
                TokenData.Range(byte.MinValue, (byte)cmbMapSight.Items.Count, Constants.ByteSize),
                TokenData.Range(byte.MinValue, (byte)cmbMapWthr.Items.Count, Constants.ByteSize),
                TokenData.Range(byte.MinValue, (byte)cmbMapType.Items.Count, Constants.ByteSize),
                TokenData.Range(byte.MinValue, (byte)cmbMapBike.Items.Count, Constants.ByteSize),
                TokenData.Range(byte.MinValue, (byte)cmbMapNameType.Items.Count, Constants.ByteSize),
                TokenData.Wildcard(1),
                TokenData.Range(byte.MinValue, (byte)cmbMapSpBg.Items.Count, Constants.ByteSize),
            };

            // マップエントリーテーブルを検証
            for (int i = 0; i < mapBankEntry.Entries.Count; i++)
            {
                // そのテーブルのエントリー数を仮カウント
                var numberEntrycount = PatternMatcher.TryCount(
                    pointerPattern, // ポインタパターン
                    _sharedData.RomData,
                    mapNumberTableOffsets[i],
                    allowNullPointer: true); // nullポインタを許容する

                // そのテーブルの正しいエントリー数を求める
                int validEntryCount = numberEntrycount; // 仮カウント数を仮代入
                for (int j = 0; j < numberEntrycount; j++)
                {
                    var pointerOffset = mapNumberTableOffsets[i] + j * entryLength;

                    // 各マップナンバーテーブルのオフセットと比較して検証
                    bool hasOffset = mapNumberTableOffsets.Contains(pointerOffset);

                    // 別のマップナンバーテーブルオフセットだった場合
                    if (j > 0 && hasOffset)
                    {
                        validEntryCount = j;
                        break;
                    }

                    // ポインタ先が正規のマップヘッダーかどうかを検証
                    if (IoHelper.TryReadPtr(_sharedData.RomData, pointerOffset, out int entryOffset))
                    {
                        // nullポインタならスキップ
                        if (entryOffset == Constants.InvalidValue) continue;

                        var IsValid = PatternMatcher.TryMatch(
                            headerPattern,
                            _sharedData.RomData,
                            entryOffset,
                            allowNullPointer: true); // nullポインタを許容する

                        // 正規のマップヘッダーでない場合
                        if (!IsValid)
                        {
                            validEntryCount = j;
                            break;
                        }
                    }
                }

                // マップナンバーエントリーテーブルを作成
                defFileName = DefName.MapNumberPointerEntry;
                var mapNumberEntry = new EntryManager(
                    defFileName,
                    typeof(FieldKey), 
                    _sharedData, 
                    mapNumberTableOffsets[i],
                    validEntryCount);

                // マップヘッダーの定義情報を読み込む
                defFileName = DefName.MapHeaderEntry;
                var mapHeaderDef = new DefReader(defFileName);

                // エントリーを作成
                var entryArray = new Entry[validEntryCount];
                for (int j = 0; j < validEntryCount; j++)
                {
                    var entryFields = new List<FieldValue>();
                    for (int k = 0; k < mapHeaderDef.FieldDefs.Count; k++)
                    {
                        // FieldValueを生成
                        var fieldValue = new FieldValue(
                            _sharedData,
                            mapHeaderDef.FieldDefs[k],
                            typeof(FieldKey));

                        entryFields.Add(fieldValue);
                    }
                    var entry = new Entry(
                        mapNumberEntry.Entries[j][FieldKey.MapHeaderOffset].GetData<int>(),
                        Constants.DefaultIndex,
                        entryFields);

                    entryArray[j] = entry;
                }

                // マップバンクに対してEntry[]を格納
                _mapHeaderEntry.Add(entryArray);
            }
        }

        private void UpdateMapNameComboBox()
        {
            // キャッシュを最新化
            LoadMapNames();

            cmbMapNameIndex.BeginUpdate();
            cmbMapNameIndex.Items.Clear();

            // キャッシュからコンボボックスへ
            foreach (var kvp in _mapNameCache)
            {
                cmbMapNameIndex.Items.Add($"[{kvp.Key:X2}]{kvp.Value}");
            }

            cmbMapNameIndex.EndUpdate();

            // 初期選択
            if (cmbMapNameIndex.Items.Count > 0)
            {
                cmbMapNameIndex.SelectedIndex = 0;
            }
        }

        private void LoadMapNames()
        {
            _mapNameCache.Clear();

            // 基準となるンデックス
            _mapNameFirstIndex = _sharedData.Config.ReadInt(IniKey.MapNameFirstIndex);

            // 順次格納していく
            for (int i = 0; i < _mapNameEntry.Entries.Count; i++)
            {
                var offset = _mapNameEntry.Entries[i][FieldKey.MapNamePointerOffset].GetData<int>();
                var mapName = _sharedData.Charmap.BytesToString(_sharedData.RomData, offset);

                int nameIndex = _mapNameFirstIndex + i;
                _mapNameCache[nameIndex] = mapName;
            }
        }

        private void UpdateMapSelector()
        {
            tvwMapSelector.BeginUpdate();
            tvwMapSelector.Nodes.Clear();

            // 番号順
            if (rbOrderByAsc.Checked)
            {
                for (int i = 0; i < _mapHeaderEntry.Count; i++)
                {
                    var bankNode = new TreeNode($"バンク{i}");

                    for (int j = 0; j < _mapHeaderEntry[i].Length; j++)
                    {
                        int nameIndex = _mapHeaderEntry[i][j][FieldKey.MapNameIndex].GetData<int>();
                        string mapName = _mapNameCache[nameIndex];

                        var mapNode = new TreeNode($"({i}, {j}) {mapName}");
                        bankNode.Nodes.Add(mapNode);
                    }
                    tvwMapSelector.Nodes.Add(bankNode);
                }
            }
            // マップ順
            else if (rbOrderByName.Checked)
            {
                var nameGroupNodes = new Dictionary<int, TreeNode>();

                for (int i = 0; i < _mapHeaderEntry.Count; i++)
                {
                    for (int j = 0; j < _mapHeaderEntry[i].Length; j++)
                    {
                        int nameIndex = _mapHeaderEntry[i][j][FieldKey.MapNameIndex].GetData<int>();

                        // ルートノードが存在しない場合は新規作成
                        if (!nameGroupNodes.ContainsKey(nameIndex))
                        {
                            string rootName = $"[{nameIndex:X2}]{_mapNameCache[nameIndex]}";
                            nameGroupNodes[nameIndex] = new TreeNode(rootName);
                        }

                        string mapName = _mapNameCache[nameIndex];
                        var mapNode = new TreeNode($"({i}, {j}) {mapName}");
                        nameGroupNodes[nameIndex].Nodes.Add(mapNode);
                    }
                }

                // マップ名IDの昇順
                foreach (var key in nameGroupNodes.Keys.OrderBy(k => k))
                {
                    tvwMapSelector.Nodes.Add(nameGroupNodes[key]);
                }
            }

            tvwMapSelector.EndUpdate();
        }
    }
}
