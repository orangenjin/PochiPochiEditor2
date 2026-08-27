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
    [FormGroup(FormGroup.Map)]
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

            InitializeEntries();
            InitializeControls();
            // InitializePipelines();
            // InitializeEventHandlers();


        }

        private void InitializeEntries()
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

            // マップヘッダーエントリー格納先を先に作成
            _mapHeaderEntry = new List<Entry[]>();

            // マップバンクテーブルを仮作成
            defFileName = DefName.MapBankPointerEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.MapBankTableOffset);
            // エントリー数を仮カウント（誤って含まれている可能性）
            var pattern = new List<TokenData>()
            {
                TokenData.Pointer()
            };
            var bankEntrycount = PatternMatcher.TryCount(
                pattern,
                _sharedData.RomData,
                tableOffset,
                allowNullPointer: false); // nullポインタを許容しない
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
                var targetOffset = 
                    mapBankEntry.Entries[i][FieldKey.MapHeaderPointerOffset].GetData<int>();

                // 正しいテーブルオフセットかどうかの検証はポインタかどうかが限界
                var IsValid = PatternMatcher.TryMatch(
                    pattern,
                    _sharedData.RomData,
                    targetOffset,
                    allowNullPointer: true); // nullポインタを許容する

                // 無効なオフセットだったら、そこで中断
                if (!IsValid) break;

                mapNumberTableOffsets.Add(targetOffset);
            }

            // 定数を事前に計算
            var entryLength = TokenData.Pointer().GetLength();

            // マップエントリーテーブルを検証
            for (int i = 0; i < mapBankEntry.Entries.Count; i++)
            {
                // そのテーブルのエントリー数を仮カウント（パターンは使い回し）
                var numberEntrycount = PatternMatcher.TryCount(
                    pattern,
                    _sharedData.RomData,
                    mapNumberTableOffsets[i],
                    allowNullPointer: true); // nullポインタを許容する

                // そのテーブルの正しいエントリー数を求める
                int trueEntryCount = numberEntrycount; // 仮カウント数
                for (int j = 0; j < numberEntrycount; j++)
                {
                    // 各テーブルの先頭オフセットと比較して検証
                    var targetOffset = mapNumberTableOffsets[i] + j * entryLength;
                    bool hasOffset = mapNumberTableOffsets.Contains(targetOffset);

                    // 別のマップナンバーテーブルオフセットだった場合
                    if (j > 0 && hasOffset)
                    {
                        trueEntryCount = j;
                        break;
                    }
                }

                // マップナンバーエントリーテーブルを作成
                defFileName = DefName.MapNumberPointerEntry;
                var mapNumberEntry =new EntryManager(
                    defFileName,
                    typeof(FieldKey), 
                    _sharedData, 
                    mapNumberTableOffsets[i],
                    trueEntryCount);

                // マップヘッダーの定義情報を読み込む
                defFileName = DefName.MapHeaderEntry;
                var mapHeaderDef = new DefReader(defFileName);

                // エントリーを作成
                var entryArray = new Entry[trueEntryCount];
                for (int j = 0; j < trueEntryCount; j++)
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

                // このインデックスのEntry[]を格納
                _mapHeaderEntry.Add(entryArray);
            }

            txtMapFooterOffset.Text = _mapHeaderEntry[42][1][FieldKey.EventScriptHeaderOffset].GetData<int>().ToString("X8");
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
            UpdateMapNameComboBox();
        }

        private void UpdateMapNameComboBox()
        {
            cmbMapNameIndex.BeginUpdate();
            cmbMapNameIndex.Items.Clear();

            // 基準となる最初のインデックスを設定
            int firstIndex = _sharedData.Config.ReadInt(IniKey.MapNameFirstIndex);

            // 順次格納していく
            for (int i = 0; i < _mapNameEntry.Entries.Count; i++)
            {
                var offset =
                    _mapNameEntry.Entries[i][FieldKey.MapNamePointerOffset].GetData<int>();
                var mapName =
                    _sharedData.Charmap.BytesToString(_sharedData.RomData, offset);

                cmbMapNameIndex.Items.Add($"[{firstIndex + i:X2}]{mapName}");
            }

            cmbMapNameIndex.EndUpdate();

            // 初期選択
            if (cmbMapNameIndex.Items.Count > 0)
            {
                cmbMapNameIndex.SelectedIndex = 0;
            }
        }
    }
}
