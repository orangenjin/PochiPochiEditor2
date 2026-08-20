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

        // 追加データ判定用
        private bool _isEncounterMusicEnabled = false;
        private bool _isBattleMusicEnabled = false;
        private bool _isPokeBallEnabled = false;
        private bool _isBaseIvEnabled = false;

        // UI制御用
        private bool _isUpdatingUI = false;
        private int _currentClassIdx = 0;

        private enum FieldKey
        {
            ClassNameStr,

            ClassNameIndex,
            PrizeMultiValue,
            PrizeMultiUnk1,
            PrizeMultiUnk2,

            EncounterMusicIndex,
            BattleMusicIndex,
            PokeBallIndex,
            BaseIvValue
        }

        private static class IniKey 
        {
            public static string TrainerClassNameEntry = nameof(TrainerClassNameEntry);
            public static string TrainerClassNameTableOffset = nameof(TrainerClassNameTableOffset);
            public static string TrainerClassNameCount = nameof(TrainerClassNameCount);

            public static string TrainerClassPrizeMultiEntry = nameof(TrainerClassPrizeMultiEntry);
            public static string TrainerClassPrizeMultiTableOffset = nameof(TrainerClassPrizeMultiTableOffset);
            public static string TrainerClassPrizeMultiCount = nameof(TrainerClassPrizeMultiCount);

            public static string TrainerClassEncMusicEntry = nameof(TrainerClassEncMusicEntry);
            public static string TrainerClassEncMusicTableOffset = nameof(TrainerClassEncMusicTableOffset);

            public static string TrainerClassBattleMusicEntry = nameof(TrainerClassBattleMusicEntry);
            public static string TrainerClassBattleMusicTableOffset = nameof(TrainerClassBattleMusicTableOffset);

            public static string TrainerClassPokeBallEntry = nameof(TrainerClassPokeBallEntry);
            public static string TrainerClassPokeBallTableOffset = nameof(TrainerClassPokeBallTableOffset);

            public static string TrainerClassBaseIvEntry = nameof(TrainerClassBaseIvEntry);
            public static string TrainerClassBaseIVTableOffset = nameof(TrainerClassBaseIVTableOffset);
        }

        public class ClassNamePipelineData
        {
            public string InputText { get; set; }
            public string FormattedText { get; set; }
            public bool IsValid { get; set; }
        }

        public TrainerClassEditor(SharedData sharedData)
        {
            InitializeComponent();
            _sharedData = sharedData;

            InitializeEntries();
            InitializeControls();
            InitializeEventHandlers();

            LoadDataToUI(_currentClassIdx);
        }

        private void InitializeEntries()
        {
            // 肩書名テーブルを作成
            string defFileName = IniKey.TrainerClassNameEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassNameTableOffset);
            int entrycount = _sharedData.Config.ReadInt(IniKey.TrainerClassNameCount);
            _className = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

            // 追加データのbool判定
            _isEncounterMusicEnabled = _sharedData.Config.ReadBool("EnableTrainerClassEncMusic");
            _isBattleMusicEnabled = _sharedData.Config.ReadBool("EnableTrainerClassBattleMusic");
            _isPokeBallEnabled = _sharedData.Config.ReadBool("EnableTrainerClassPokeBall");
            _isBaseIvEnabled = _sharedData.Config.ReadBool("EnableTrainerClassBaseIV");

            if (_isEncounterMusicEnabled)
            {
                // 戦闘前BGMテーブルを作成
                defFileName = IniKey.TrainerClassEncMusicEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassEncMusicTableOffset);
                _encMusic = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isBattleMusicEnabled)
            {
                // 戦闘中BGMテーブルを作成
                defFileName = IniKey.TrainerClassBattleMusicEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassBattleMusicTableOffset);
                _battleMusic = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isPokeBallEnabled)
            {
                // 使用ボールIDテーブルを作成
                defFileName = IniKey.TrainerClassPokeBallEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassPokeBallTableOffset);
                _pokeBall = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isBaseIvEnabled)
            {
                // 基礎個体値テーブルを作成
                defFileName = IniKey.TrainerClassBaseIvEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassBaseIVTableOffset);
                _baseIv = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            // 賞金倍率テーブルを作成
            defFileName = IniKey.TrainerClassPrizeMultiEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassPrizeMultiTableOffset);
            entrycount = _sharedData.Config.ReadInt(IniKey.TrainerClassPrizeMultiCount);
            _prizeMulti = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
        }

        private void InitializeControls()
        {
            // クラス名をcmbに格納
            var classNames = _className.Entries
                .Select(entry => entry[FieldKey.ClassNameStr].GetData<string>())
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

        private void InitializePipelines()
        {
            var txtClassNameOrder = new UiCtrlManager<ClassNamePipelineData>()
                // input
                .Then(ctx =>
                {
                    var textBox = (TextBox)ctx.Sender;
                    ctx.Data.InputText = textBox.Text;
                })
                // calc
                .Then(ctx =>
                {
                    ctx.Data.FormattedText = ctx.Data.InputText.Trim().ToUpper();
                    ctx.Data.IsValid = ctx.Data.FormattedText.Length > 0;
                });

            // イベントハンドラーの登録
            txtClassName.TextChanged += (s, e) =>
            {
                txtClassNameOrder.Execute(
                    new UiContext<ClassNamePipelineData>(s, e, UpdateReason.Ctrl));
            };
        }

        private void LoadDataToUI(int index)
        {
            _isUpdatingUI = true;

            _currentClassIdx = index;
            cmbClassNameIndex.SelectedIndex = index;
            nudClassNameIndex.Value = (decimal)index;




            _isUpdatingUI = false;
        }

        private void cmbClassNameIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUI) return;

            int newIndex = cmbClassNameIndex.SelectedIndex;
            LoadDataToUI(newIndex);
        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUI) return;

            string validName = _className.Entries[_currentClassIdx][FieldKey.ClassNameStr].GetData<string>();
            cmbClassNameIndex.Items[_currentClassIdx] = validName;
        }

        /*
         *             _bindingManager?.Dispose();
            _bindingManager = new BindingManager(grpBasicData);

            // クラス名
            _bindingManager.AddBinding<string>(_className.Entries[index][FieldKey.ClassName]);

            // 賞金倍率、インデックス調整あり
            int foundIndex = _prizeMulti.Entries
                .FindIndex(entry => entry[FieldKey.ClassNameIndex]
                .GetData<int>() == index);
            if (foundIndex == Constants.InvalidValue) // 存在せず、クラス名インデックス0xFF適用パターン
            {
                foundIndex = _prizeMulti.Entries
                    .FindIndex(entry => entry[FieldKey.ClassNameIndex]
                    .GetData<int>() == 0xFF);
            }
            _bindingManager.AddBinding<int>(_prizeMulti.Entries[foundIndex][FieldKey.PrizeMulti]);
         * 
         * 
         * 
         * 
         * */
    }
}
