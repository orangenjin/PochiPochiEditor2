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

        private enum FieldKey
        {
            MapNamePointerOffset,
        }

        private static class IniKey
        {
            public static string MapNamePointerEntry = nameof(MapNamePointerEntry);
            public static string MapNameTableOffset = nameof(MapNameTableOffset);
            public static string MapNameCount = nameof(MapNameCount);
            public static string MapNameFirstIndex = nameof(MapNameFirstIndex);
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
            string defFileName = IniKey.MapNamePointerEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.MapNameTableOffset);
            int entrycount = _sharedData.Config.ReadInt(IniKey.MapNameCount);
            _mapNameEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

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
