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
using PochiPochiEditor2.Managers.UiControls;
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
            public static string TrainerClassNameEntryLength = nameof(TrainerClassNameEntryLength);

            public static string TrainerClassPrizeMultiEntry = nameof(TrainerClassPrizeMultiEntry);
            public static string TrainerClassPrizeMultiTableOffset = nameof(TrainerClassPrizeMultiTableOffset);
            public static string TrainerClassPrizeMultiCount = nameof(TrainerClassPrizeMultiCount);

            public static string EnableTrainerClassEncMusic = nameof(EnableTrainerClassEncMusic);
            public static string TrainerClassEncMusicEntry = nameof(TrainerClassEncMusicEntry);
            public static string TrainerClassEncMusicTableOffset = nameof(TrainerClassEncMusicTableOffset);

            public static string EnableTrainerClassBattleMusic = nameof(EnableTrainerClassBattleMusic);
            public static string TrainerClassBattleMusicEntry = nameof(TrainerClassBattleMusicEntry);
            public static string TrainerClassBattleMusicTableOffset = nameof(TrainerClassBattleMusicTableOffset);

            public static string EnableTrainerClassPokeBall = nameof(EnableTrainerClassPokeBall);
            public static string TrainerClassPokeBallEntry = nameof(TrainerClassPokeBallEntry);
            public static string TrainerClassPokeBallTableOffset = nameof(TrainerClassPokeBallTableOffset);

            public static string EnableTrainerClassBaseIV = nameof(EnableTrainerClassBaseIV);
            public static string TrainerClassBaseIvEntry = nameof(TrainerClassBaseIvEntry);
            public static string TrainerClassBaseIVTableOffset = nameof(TrainerClassBaseIVTableOffset);
        }

        public TrainerClassEditor(SharedData sharedData)
        {
            InitializeComponent();
            _sharedData = sharedData;

            InitializeEntries();
            InitializeControls();
            InitializePipelines();
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
            _isEncounterMusicEnabled = _sharedData.Config.ReadBool(IniKey.EnableTrainerClassEncMusic);
            _isBattleMusicEnabled = _sharedData.Config.ReadBool(IniKey.EnableTrainerClassBattleMusic);
            _isPokeBallEnabled = _sharedData.Config.ReadBool(IniKey.EnableTrainerClassPokeBall);
            _isBaseIvEnabled = _sharedData.Config.ReadBool(IniKey.EnableTrainerClassBaseIV);

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

        private void InitializePipelines()
        {
            // txtClassName
            {
                var uiPipeline = new UiPipeline<ClassNamePipelineData>()
                    // 入力値を取得
                    .Then(ctx =>
                    {
                        var textBox = (TextBox)ctx.Sender;
                        ctx.Data.InputText = textBox.Text;
                    })
                    // 長さを調整
                    .Then(ctx =>
                    {
                        int entryLength = _sharedData.Config.ReadInt(
                            IniKey.TrainerClassNameEntryLength);
                        ctx.Data.FormattedText = CalcHelper.TextLengthValidate(
                            _sharedData.Charmap,
                            ctx.Data.InputText,
                            entryLength);
                    })
                    // データとテキストボックスを更新
                    .Then(ctx =>
                    {
                        _className.Entries[_currentClassIdx][FieldKey.ClassNameStr].SetData(ctx.Data.FormattedText);
                        txtClassName.Text = ctx.Data.FormattedText;
                        CtrlHelper.MoveCursorToEnd(txtClassName); // カーソル位置
                    })
                    // コンボボックスを更新
                    .Then(ctx =>
                    {
                        cmbClassNameIndex.Items[_currentClassIdx] = ctx.Data.FormattedText;
                    });
                // イベントハンドラーの登録
                _eventBinder.BindCtrl(
                    h => txtClassName.TextChanged += h,
                    h => txtClassName.TextChanged -= h,
                    (s, e) =>
                    {
                        uiPipeline.Execute(
                            new UiContext<ClassNamePipelineData>(s, e, UpdateReason.Ctrl));
                    });
            }
        }

        private void InitializeEventHandlers()
        {
            // クラス名インデックスcmb
            _eventBinder.BindCtrl(
                h => cmbClassNameIndex.SelectedIndexChanged += h,
                h => cmbClassNameIndex.SelectedIndexChanged -= h,
                (s, e) =>
                {
                    if (_isUpdatingUI) return;

                    int newIndex = cmbClassNameIndex.SelectedIndex;
                    LoadDataToUI(newIndex);
                });

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }



        private void LoadDataToUI(int index)
        {
            _isUpdatingUI = true;

            // インデックス
            _currentClassIdx = index;
            cmbClassNameIndex.SelectedIndex = index;
            nudClassNameIndex.Value = (decimal)index;

            // クラス名
            txtClassName.Text = _className.Entries[index][FieldKey.ClassNameStr].GetData<string>();

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

            nudPrizeMulti.Value = (decimal)_prizeMulti.Entries[foundIndex][FieldKey.PrizeMultiValue].GetData<int>();

            _isUpdatingUI = false;
        }
    }
}
