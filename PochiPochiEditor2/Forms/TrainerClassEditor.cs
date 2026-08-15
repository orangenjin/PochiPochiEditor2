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
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Forms
{
    [FormGroup(FormGroup.TrainerClass)]
    public partial class TrainerClassEditor : Form
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;
        // 各テーブル用
        private EntryManager _className = null;
        private EntryManager _prizeMulti = null;
        private EntryManager _encMusic = null;
        private EntryManager _battleMusic = null;
        private EntryManager _pokeBall = null;
        private EntryManager _baseIv = null;

        // バインディング
        private BindingManager _bindingManager = null;

        // 追加データ判定用
        private bool _isEncounterMusicEnabled = false;
        private bool _isBattleMusicEnabled = false;
        private bool _isPokeBallEnabled = false;
        private bool _isBaseIvEnabled = false;


        private enum FieldKey
        {
            ClassName,
            PrizeMulti,
            Padding1
        }

        public TrainerClassEditor(SharedData sharedData)
        {
            InitializeComponent();
            _sharedData = sharedData;

            InitializeEntries();
            InitializeControls();
            InitializeEventHandlers();
            InitializeBindings();
        }

        private void InitializeEntries()
        {
            // 肩書名テーブルを作成
            string defFileName = "TrainerClassNameEntry";
            int tableOffset = _sharedData.Config.ReadInt("TrainerClassNameTableOffset");
            int entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
            _className = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);

            // 賞金倍率テーブルを作成
            defFileName = "TrainerClassPrizeMultiEntry";
            tableOffset = _sharedData.Config.ReadInt("TrainerClassPrizeMultiTableOffset");
            entrycount = _sharedData.Config.ReadInt("TrainerClassPrizeMultiCount");
            _prizeMulti = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);

            // 追加データのbool判定
            _isEncounterMusicEnabled = _sharedData.Config.ReadBool("EnableTrainerClassEncMusic");
            _isBattleMusicEnabled = _sharedData.Config.ReadBool("EnableTrainerClassBattleMusic");
            _isPokeBallEnabled = _sharedData.Config.ReadBool("EnableTrainerClassPokeBall");
            _isBaseIvEnabled = _sharedData.Config.ReadBool("EnableTrainerClassBaseIV");

            if (_isEncounterMusicEnabled)
            {
                // 戦闘前BGMテーブルを作成
                defFileName = "TrainerClassEncMusicEntry";
                tableOffset = _sharedData.Config.ReadInt("TrainerClassEncMusicTableOffset");
                entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
                _encMusic = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);
            }

            if (_isBattleMusicEnabled)
            {
                // 戦闘中BGMテーブルを作成
                defFileName = "TrainerClassBattleMusicEntry";
                tableOffset = _sharedData.Config.ReadInt("TrainerClassBattleMusicTableOffset");
                entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
                _battleMusic = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);
            }

            if (_isPokeBallEnabled)
            {
                // 使用ボールIDテーブルを作成
                defFileName = "TrainerClassPokeBallEntry";
                tableOffset = _sharedData.Config.ReadInt("TrainerClassPokeBallTableOffset");
                entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
                _pokeBall = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);
            }

            if (_isBaseIvEnabled)
            {
                // 基礎個体値テーブルを作成
                defFileName = "TrainerClassBaseIvEntry";
                tableOffset = _sharedData.Config.ReadInt("TrainerClassBaseIVTableOffset");
                entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
                _baseIv = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);
            }
        }

        private void InitializeControls()
        {
            // クラス名をcmbに格納
            var classNames = _className.Entries
                .Select(entry => entry[FieldKey.ClassName].GetData<string>())
                .ToArray();
            cmbClassNameIndex.Items.AddRange(classNames);

            // 追加データのctrlの無効化
            if (!_isEncounterMusicEnabled)
            {
                lblEncMusic.Enabled = false;
                nudEncMusic.Enabled = false;
            }

            if (!_isBattleMusicEnabled)
            {
                lblBattleMusic.Enabled = false;
                nudBattleMusic.Enabled = false;
            }

            if (!_isPokeBallEnabled)
            {
                lblPokeBall.Enabled = false;
                nudPokeBall.Enabled = false;
            }

            if (!_isBaseIvEnabled)
            {
                lblBaseIv.Enabled = false;
                nudBaseIv.Enabled = false;
            }
        }

        private void InitializeEventHandlers()
        {
            // クラス名インデックスcmb
            _eventBinder.BindCtrl(
                h => cmbClassNameIndex.SelectedIndexChanged += h,
                h => cmbClassNameIndex.SelectedIndexChanged -= h,
                cmbClassNameIndex_SelectedIndexChanged);
            // クラス名txt
            _eventBinder.BindCtrl(
                h => txtClassName.TextChanged += h,
                h => txtClassName.TextChanged -= h,
                txtClassName_TextChanged);

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void InitializeBindings()
        {
            // コンストラクタ
            _bindingManager = new BindingManager(this);


            // _bindingManager.AddBinding<string>(_className.Entries[5][FieldKey.ClassName]);
        }

        private void LoadDataToUI(int idx)
        {

        }

        private void cmbClassNameIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
