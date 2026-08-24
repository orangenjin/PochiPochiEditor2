using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Forms
{
    [FormGroup(FormGroup.TrainerClass)]
    public partial class TrainerClassEditor : Form, IEditorRefresh
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;
        // 変更履歴用
        private UndoManager _undoManager = null;
        // 各テーブル用
        private EntryManager _className = null;
        private EntryManager _prizeMulti = null;
        private EntryManager _encMusic = null;
        private EntryManager _battleMusic = null;
        private EntryManager _pokeBall = null;
        private EntryManager _baseIv = null;
        // パイプライン用
        private UiPipelineBuilder _classNamePipeline = null;
        private UiPipelineBuilder _prizeMultiPipeline = null;
        private UiPipelineBuilder _encMusicPipeline = null;
        private UiPipelineBuilder _battleMusicPipeline = null;
        private UiPipelineBuilder _pokeBallPipeline = null;
        private UiPipelineBuilder _baseIvPipeline = null;

        // 追加データ判定用
        private bool _isEncounterMusicEnabled = false;
        private bool _isBattleMusicEnabled = false;
        private bool _isPokeBallEnabled = false;
        private bool _isBaseIvEnabled = false;

        // UI制御用
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

        public TrainerClassEditor(SharedData sharedData, UndoManager undoManager)
        {
            InitializeComponent();
            _sharedData = sharedData;
            _undoManager = undoManager;

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
            UpdateClassNameComboBox();

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

        private void UpdateClassNameComboBox()
        {
            // クラス名をcmbに格納
            var classNames = _className.Entries
                .Select(entry => entry[FieldKey.ClassNameStr].GetData<string>())
                .ToArray();
            cmbClassNameIndex.Items.AddRange(classNames);
        }

        private void InitializePipelines()
        {
            // txtClassName
            _classNamePipeline = new UiPipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((TextBox)ctx.Sender); // テキストボックス
                    ctx.Set(ctx.Get<TextBox>().Text); // 入力されたテキスト
                })
                // 長さを調整
                .Then(ctx =>
                {
                    // AlloewedLengthはない
                    int entryLength = _className.Entries[_currentClassIdx][FieldKey.ClassNameStr].EntryLength;
                    var formattedText = CalcHelper.TextLengthValidate(
                            _sharedData.Charmap,
                            ctx.Get<string>(),
                            entryLength);
                    ctx.Set(formattedText); // トリムされたテキスト
                })
                // データを更新
                .Then(ctx =>
                {
                    var targetField = _className.Entries[_currentClassIdx][FieldKey.ClassNameStr];

                    // 変更前のバイナリデータ
                    byte[] oldBinary = targetField.BinaryData;

                    // データ更新
                    targetField.SetData(ctx.Get<string>());

                    // 変更後のバイナリデータ
                    byte[] newBinary = targetField.BinaryData;

                    // 異なればスタックに追加
                    if (!oldBinary.SequenceEqual(newBinary))
                    {
                        var cmd = new FieldChangeCommand(
                            targetField,
                            oldBinary,
                            newBinary,
                            $"[{this.Text}]肩書き名(ID:{_currentClassIdx})");
                        _undoManager.PushCommand(cmd);
                    }
                })
                // テキストボックスを更新
                .Then(ctx =>
                {
                    ctx.Get<TextBox>().Text = ctx.Get<string>();
                    CtrlHelper.MoveCursorToEnd(ctx.Get<TextBox>()); // カーソル位置
                })
                // コンボボックスを更新（副次的）
                .Then(ctx =>
                {
                    cmbClassNameIndex.Items[_currentClassIdx] = ctx.Get<string>();
                });

            // nudPrizeMulti
            _prizeMultiPipeline = new UiPipelineBuilder()
                // 入力値を取得、データを更新
                .Then(ctx =>
                {
                    // 入力値
                    var nud = (NumericUpDown)ctx.Sender;
                    int newValue = (int)nud.Value;

                    // 対象のインデックスを計算
                    int calcIndex = CalcPrizeMultiIndex(_currentClassIdx);

                    // 対象データ
                    var targetField = _prizeMulti.Entries[calcIndex][FieldKey.PrizeMultiValue];

                    // 変更前のバイナリデータ
                    byte[] oldBinary = targetField.BinaryData;

                    // データ更新
                    targetField.SetData(newValue);

                    // 変更後のバイナリデータ
                    byte[] newBinary = targetField.BinaryData;

                    // 異なればスタックに追加
                    if (!oldBinary.SequenceEqual(newBinary))
                    {
                        var cmd = new FieldChangeCommand(
                            targetField,
                            oldBinary,
                            newBinary,
                            $"[{this.Text}]賞金倍率(ID:{_currentClassIdx})");
                        _undoManager.PushCommand(cmd);
                    }

                    // UI更新
                    nud.Value = newValue;
                });

            // ループで回すためのタプル
            var pipelineConfigs = new (
                bool IsEnabled,
                EntryManager Entry,
                FieldKey Key,
                Func<string> DescGenerator,
                Action<UiPipelineBuilder> AssignPipeline)[]
            {
                (_isEncounterMusicEnabled, 
                    _encMusic, 
                    FieldKey.EncounterMusicIndex,
                    () => $"[{this.Text}]戦闘前BGM(ID:{_currentClassIdx})",
                    p => _encMusicPipeline = p),
                (_isBattleMusicEnabled, 
                    _battleMusic, 
                    FieldKey.BattleMusicIndex,
                    () => $"[{this.Text}]戦闘中BGM(ID:{_currentClassIdx})",
                    p => _battleMusicPipeline = p),
                (_isPokeBallEnabled, 
                    _pokeBall,
                    FieldKey.PokeBallIndex,
                    () => $"[{this.Text}]使用ボールID(ID:{_currentClassIdx})",
                    p => _pokeBallPipeline = p),
                (_isBaseIvEnabled, 
                    _baseIv, 
                    FieldKey.BaseIvValue,
                    () => $"[{this.Text}]基礎個体値(ID:{_currentClassIdx})", 
                    p => _baseIvPipeline = p)
            };

            // 追加データのループ処理
            foreach (var config in pipelineConfigs)
            {
                if (config.IsEnabled)
                {
                    var pipeline = BuildPipeline(config.Entry, config.Key, config.DescGenerator);
                    config.AssignPipeline(pipeline);
                }
            }

            // ループヘルパー
            UiPipelineBuilder BuildPipeline(EntryManager entry, FieldKey key, Func<string> descGen)
            {
                return new UiPipelineBuilder()
                    .Then(ctx =>
                    {
                        // 入力値
                        var nud = (NumericUpDown)ctx.Sender;
                        int newValue = (int)nud.Value;

                        // 対象データ
                        var targetField = entry.Entries[_currentClassIdx][key];

                        // 変更前のバイナリデータ
                        byte[] oldBinary = targetField.BinaryData;

                        // データ更新
                        targetField.SetData(newValue);

                        // 変更後のバイナリデータ
                        byte[] newBinary = targetField.BinaryData;

                        // 異なればスタックに追加
                        if (!oldBinary.SequenceEqual(newBinary))
                        {
                            var cmd = new FieldChangeCommand(
                                targetField,
                                oldBinary,
                                newBinary,
                                descGen());

                            _undoManager.PushCommand(cmd);
                        }

                        // UI更新
                        nud.Value = newValue;
                    });
            }
        }

        private void InitializeEventHandlers()
        {
            // クラス名インデックスcmb
            _eventBinder.BindCtrl(
                h => cmbClassNameIndex.SelectedIndexChanged += h,
                h => cmbClassNameIndex.SelectedIndexChanged -= h,
                (_, __) =>
                {
                    int newIndex = cmbClassNameIndex.SelectedIndex;
                    LoadDataToUI(newIndex);
                });

            // 肩書き名
            _eventBinder.BindCtrl(
                h => txtClassName.TextChanged += h,
                h => txtClassName.TextChanged -= h,
                (s, e) =>
                {
                    _classNamePipeline.Execute(new UiContext(s, e));
                });

            // 賞金倍率
            _eventBinder.BindCtrl(
                h => nudPrizeMulti.ValueChanged += h,
                h => nudPrizeMulti.ValueChanged -= h,
                (s, e) =>
                {
                    _prizeMultiPipeline.Execute(new UiContext(s, e));
                });

            // 追加データ関連
            if (_isEncounterMusicEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudEncMusic.ValueChanged += h,
                    h => nudEncMusic.ValueChanged -= h,
                    (s, e) =>
                    {
                        _encMusicPipeline.Execute(new UiContext(s, e));
                    });
            }

            if (_isBattleMusicEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudBattleMusic.ValueChanged += h,
                    h => nudBattleMusic.ValueChanged -= h,
                    (s, e) =>
                    {
                        _battleMusicPipeline.Execute(new UiContext(s, e));
                    });
            }

            if (_isPokeBallEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudPokeBall.ValueChanged += h,
                    h => nudPokeBall.ValueChanged -= h,
                    (s, e) =>
                    {
                        _pokeBallPipeline.Execute(new UiContext(s, e));
                    });
            }

            if (_isBaseIvEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudBaseIv.ValueChanged += h,
                    h => nudBaseIv.ValueChanged -= h,
                    (s, e) =>
                    {
                        _baseIvPipeline.Execute(new UiContext(s, e));
                    });
            }

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void LoadDataToUI(int index)
        {
            // インデックス
            _currentClassIdx = index;
            cmbClassNameIndex.SelectedIndex = index;
            nudClassNameIndex.Value = (decimal)index;

            // クラス名
            txtClassName.Text = _className.Entries[index][FieldKey.ClassNameStr].GetData<string>();

            // 賞金倍率、インデックス計算あり
            int calcIndex = CalcPrizeMultiIndex(index);
            nudPrizeMulti.Value = (decimal)_prizeMulti.Entries[calcIndex][FieldKey.PrizeMultiValue].GetData<int>();

            // 追加データ
            if (_isEncounterMusicEnabled)
            {
                nudEncMusic.Value = (decimal)_encMusic.Entries[index][FieldKey.EncounterMusicIndex].GetData<int>();
            }

            if (_isBattleMusicEnabled)
            {
                nudBattleMusic.Value = (decimal)_battleMusic.Entries[index][FieldKey.BattleMusicIndex].GetData<int>();
            }

            if (_isPokeBallEnabled)
            {
                nudPokeBall.Value = (decimal)_pokeBall.Entries[index][FieldKey.PokeBallIndex].GetData<int>();
            }

            if (_isBaseIvEnabled)
            {
                nudBaseIv.Value = (decimal)_baseIv.Entries[index][FieldKey.BaseIvValue].GetData<int>();
            }
        }

        /// <summary>
        /// 賞金倍率のインデックスを計算する。
        /// </summary>
        private int CalcPrizeMultiIndex(int index)
        {
            // 総エントリーからindexを含むエントリーを探す
            int foundIndex = _prizeMulti.Entries
                .FindIndex(entry => entry[FieldKey.ClassNameIndex]
                .GetData<int>() == index);

            if (foundIndex == Constants.InvalidValue) // 存在せず、クラス名インデックス0xFF適用パターン
            {
                foundIndex = _prizeMulti.Entries
                    .FindIndex(entry => entry[FieldKey.ClassNameIndex]
                    .GetData<int>() == 0xFF);
            }

            return foundIndex;
        }

        /// <summary>
        /// FormGroupManagerからのUI再描画用の処理。
        /// </summary>
        public void RefreshFromData()
        {
            UpdateClassNameComboBox();

            // 現在のインデックスを再読み込み
            LoadDataToUI(_currentClassIdx);
        }
    }
}
