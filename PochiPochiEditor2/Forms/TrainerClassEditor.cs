using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
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
        private EntryManager _classNameEntry = null;
        private EntryManager _prizeMultiEntry = null;
        private EntryManager _encMusicEntry = null;
        private EntryManager _battleMusicEntry = null;
        private EntryManager _pokeBallEntry = null;
        private EntryManager _baseIvEntry = null;
        // パイプライン用
        private PipelineBuilder _classNamePipeline = null;
        private PipelineBuilder _prizeMultiPipeline = null;
        private PipelineBuilder _encMusicPipeline = null;
        private PipelineBuilder _battleMusicPipeline = null;
        private PipelineBuilder _pokeBallPipeline = null;
        private PipelineBuilder _baseIvPipeline = null;
        // 追加データ判定用
        private bool _isEncounterMusicEnabled = false;
        private bool _isBattleMusicEnabled = false;
        private bool _isPokeBallEnabled = false;
        private bool _isBaseIvEnabled = false;
        // UI制御用
        private int _currentClassIndex = 0;

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

            LoadDataToUI(_currentClassIndex);
        }

        private void InitializeEntries()
        {
            // 肩書名テーブルを作成
            string defFileName = IniKey.TrainerClassNameEntry;
            int tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassNameTableOffset);
            int entrycount = _sharedData.Config.ReadInt(IniKey.TrainerClassNameCount);
            _classNameEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);

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
                _encMusicEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isBattleMusicEnabled)
            {
                // 戦闘中BGMテーブルを作成
                defFileName = IniKey.TrainerClassBattleMusicEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassBattleMusicTableOffset);
                _battleMusicEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isPokeBallEnabled)
            {
                // 使用ボールIDテーブルを作成
                defFileName = IniKey.TrainerClassPokeBallEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassPokeBallTableOffset);
                _pokeBallEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            if (_isBaseIvEnabled)
            {
                // 基礎個体値テーブルを作成
                defFileName = IniKey.TrainerClassBaseIvEntry;
                tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassBaseIVTableOffset);
                _baseIvEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
            }

            // 賞金倍率テーブルを作成
            defFileName = IniKey.TrainerClassPrizeMultiEntry;
            tableOffset = _sharedData.Config.ReadInt(IniKey.TrainerClassPrizeMultiTableOffset);
            entrycount = _sharedData.Config.ReadInt(IniKey.TrainerClassPrizeMultiCount);
            _prizeMultiEntry = new EntryManager(defFileName, typeof(FieldKey), _sharedData, tableOffset, entrycount);
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
            cmbClassNameIndex.BeginUpdate();
            cmbClassNameIndex.Items.Clear();

            var classNames = _classNameEntry.Entries
                .Select(entry => entry[FieldKey.ClassNameStr].GetData<string>())
                .ToArray();

            cmbClassNameIndex.Items.AddRange(classNames);
            cmbClassNameIndex.EndUpdate();

            // 再選択
            if (cmbClassNameIndex.Items.Count > 0)
            {
                cmbClassNameIndex.SelectedIndex = _currentClassIndex;
            }
        }

        private void InitializePipelines()
        {
            // txtClassName
            _classNamePipeline = new PipelineBuilder()
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
                    int entryLength = _classNameEntry.Entries[_currentClassIndex][FieldKey.ClassNameStr].EntryLength;
                    var formattedText = CalcHelper.TextLengthValidate(
                            _sharedData.Charmap,
                            ctx.Get<string>(),
                            entryLength);
                    ctx.Set(formattedText); // トリムされたテキスト
                })
                // データを更新
                .Then(ctx =>
                {
                    var desc = $"[{this.Text}]肩書き名(ID:{_currentClassIndex:D4})";
                    _classNameEntry.Entries[_currentClassIndex][FieldKey.ClassNameStr]
                        .UpdateData(_undoManager, ctx.Get<string>(), desc);
                });

            // nudPrizeMulti
            _prizeMultiPipeline = new PipelineBuilder()
                // 入力値を取得
                .Then(ctx =>
                {
                    ctx.Set((NumericUpDown)ctx.Sender); // ニューメリックアップダウン
                    ctx.Set((int)ctx.Get<NumericUpDown>().Value); // 入力された値
                })
                // データを更新
                .Then(ctx =>
                {
                    // 対象のインデックスを計算
                    int calcIndex = CalcPrizeMultiIndex(_currentClassIndex);

                    var desc = $"[{this.Text}]賞金倍率(ID:{_currentClassIndex:D4})";
                    _prizeMultiEntry.Entries[calcIndex][FieldKey.PrizeMultiValue]
                        .UpdateData(_undoManager, ctx.Get<int>(), desc);
                });

            // ループで回すためのタプル
            var pipelineConfigs = new (
                bool IsEnabled,
                EntryManager Entry,
                FieldKey Key,
                Func<string> DescGenerator,
                Action<PipelineBuilder> AssignPipeline)[]
            {
                (_isEncounterMusicEnabled, 
                    _encMusicEntry, 
                    FieldKey.EncounterMusicIndex,
                    () => $"[{this.Text}]戦闘前BGM(ID:{_currentClassIndex:D4})",
                    p => _encMusicPipeline = p),
                (_isBattleMusicEnabled,
                    _encMusicEntry, 
                    FieldKey.BattleMusicIndex,
                    () => $"[{this.Text}]戦闘中BGM(ID:{_currentClassIndex:D4})",
                    p => _battleMusicPipeline = p),
                (_isPokeBallEnabled,
                    _encMusicEntry,
                    FieldKey.PokeBallIndex,
                    () => $"[{this.Text}]使用ボールID(ID:{_currentClassIndex:D4})",
                    p => _pokeBallPipeline = p),
                (_isBaseIvEnabled,
                    _encMusicEntry, 
                    FieldKey.BaseIvValue,
                    () => $"[{this.Text}]基礎個体値(ID:{_currentClassIndex:D4})", 
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
            PipelineBuilder BuildPipeline(EntryManager entry, FieldKey key, Func<string> descGen)
            {
                return new PipelineBuilder()
                    .Then(ctx =>
                    {
                        // 入力値
                        var nud = (NumericUpDown)ctx.Sender;
                        int newValue = (int)nud.Value;

                        // データを更新
                        entry.Entries[_currentClassIndex][key]
                            .UpdateData(_undoManager, newValue, descGen());
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
                h => txtClassName.Validated += h,
                h => txtClassName.Validated -= h,
                (s, e) =>
                {
                    _classNamePipeline.Execute(new Context(s, e));
                });

            // 賞金倍率
            _eventBinder.BindCtrl(
                h => nudPrizeMulti.ValueChanged += h,
                h => nudPrizeMulti.ValueChanged -= h,
                (s, e) =>
                {
                    _prizeMultiPipeline.Execute(new Context(s, e));
                });

            // 追加データ関連
            if (_isEncounterMusicEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudEncMusic.ValueChanged += h,
                    h => nudEncMusic.ValueChanged -= h,
                    (s, e) =>
                    {
                        _encMusicPipeline.Execute(new Context(s, e));
                    });
            }

            if (_isBattleMusicEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudBattleMusic.ValueChanged += h,
                    h => nudBattleMusic.ValueChanged -= h,
                    (s, e) =>
                    {
                        _battleMusicPipeline.Execute(new Context(s, e));
                    });
            }

            if (_isPokeBallEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudPokeBall.ValueChanged += h,
                    h => nudPokeBall.ValueChanged -= h,
                    (s, e) =>
                    {
                        _pokeBallPipeline.Execute(new Context(s, e));
                    });
            }

            if (_isBaseIvEnabled)
            {
                _eventBinder.BindCtrl(
                    h => nudBaseIv.ValueChanged += h,
                    h => nudBaseIv.ValueChanged -= h,
                    (s, e) =>
                    {
                        _baseIvPipeline.Execute(new Context(s, e));
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
            _currentClassIndex = index;
            cmbClassNameIndex.SelectedIndex = index;
            nudClassNameIndex.Value = (decimal)index;

            // クラス名
            txtClassName.Text = 
                _classNameEntry.Entries[index][FieldKey.ClassNameStr].GetData<string>();

            // 賞金倍率、インデックス計算あり
            int calcIndex = CalcPrizeMultiIndex(index);
            nudPrizeMulti.Value = 
                (decimal)_prizeMultiEntry.Entries[calcIndex][FieldKey.PrizeMultiValue].GetData<int>();

            // 追加データ
            if (_isEncounterMusicEnabled)
            {
                nudEncMusic.Value = 
                    (decimal)_encMusicEntry.Entries[index][FieldKey.EncounterMusicIndex].GetData<int>();
            }

            if (_isBattleMusicEnabled)
            {
                nudBattleMusic.Value = 
                    (decimal)_battleMusicEntry.Entries[index][FieldKey.BattleMusicIndex].GetData<int>();
            }

            if (_isPokeBallEnabled)
            {
                nudPokeBall.Value = 
                    (decimal)_pokeBallEntry.Entries[index][FieldKey.PokeBallIndex].GetData<int>();
            }

            if (_isBaseIvEnabled)
            {
                nudBaseIv.Value = 
                    (decimal)_baseIvEntry.Entries[index][FieldKey.BaseIvValue].GetData<int>();
            }
        }

        /// <summary>
        /// 賞金倍率のインデックスを計算する。
        /// </summary>
        private int CalcPrizeMultiIndex(int index)
        {
            // 総エントリーからindexを含むエントリーを探す
            int foundIndex = _prizeMultiEntry.Entries
                .FindIndex(entry => entry[FieldKey.ClassNameIndex]
                .GetData<int>() == index);

            if (foundIndex == Constants.InvalidValue) // 存在せず、クラス名インデックス0xFF適用パターン
            {
                foundIndex = _prizeMultiEntry.Entries
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
            // 現在のインデックスを再読み込み
            LoadDataToUI(_currentClassIndex);

            UpdateClassNameComboBox();
        }
    }
}
