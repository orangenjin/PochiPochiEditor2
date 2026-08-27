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
        private EntryManager _headerEntry = null;

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

        }

        private void InitializeControls()
        {
            CtrlHelper.LoadComboBoxFromFile(
                (cmbMapType, "txt/Map/MapType.txt"),
                (cmbMapWthr, "txt/Map/MapWthr.txt"),
                (cmbMapSight, "txt/Map/MapSight.txt"),
                (cmbMapBike, "txt/Map/MapBike.txt"),
                (cmbMapSpBg, "txt/Map/MapSpBg.txt"),
                (cmbMapNameType, "txt/Map/MapNameType.txt"));
        }
    }
}
