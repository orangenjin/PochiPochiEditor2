using System.Windows.Forms;

using PochiPochiEditor2.Helpers;

namespace PochiPochiEditor2.Utilities
{
    public partial class QuickInput : Form
    {
        public int Offset { get; set; }
        public int DataTypeIndex { get; set; }
        public int EntryCount { get; set; }
        public string FilePath { get; set; }

        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // パイプライン用
        private PipelineBuilder _targetOffsetPipeline = null;

        // ファイルパスを保持
        private readonly string _fileFilter;

        /// <summary>
        /// 引数はフォームを形成するのに必要なもの。
        /// あくまで、入力値を取得するだけ。
        /// </summary>
        public QuickInput(
            int? defaultOffset = null,
            string[] cmbItems = null,
            decimal? nudMin = null,
            decimal? nudMax = null,
            string fileFilter = null)
        {
            InitializeComponent();

            InitializePipelines();
            InitializeEventHandlers();

            // 一時的にすべてを無効化
            CtrlHelper.SetControlsEnabled(
                grpInputInfo, 
                enabled: false, 
                includeSelf: false);

            // 各コントロールを有効化
            SetupOffsetInput(defaultOffset);
            SetupComboBox(cmbItems);
            SetupNumericUpDown(nudMin, nudMax);
            _fileFilter = fileFilter;
            SetupFileInput(_fileFilter);
        }

        private void InitializePipelines()
        {
            _targetOffsetPipeline = new PipelineBuilder()
                // 入力値を取得、整形を行う
                .Then(ctx =>
                {
                    // 入力値
                    var txt = (TextBox)ctx.Sender;
                    string valueStr = txt.Text;

                    // 整形
                    txt.Text = valueStr.ParseStringToInt().ParseIntToString();
                    CtrlHelper.MoveCursorToEnd(txt); // カーソル位置
                });
        }

        private void InitializeEventHandlers()
        {
            _eventBinder.BindCtrl(
                h => txtTargetOffset.TextChanged += h,
                h => txtTargetOffset.TextChanged -= h,
                (s, e) =>
                {
                    _targetOffsetPipeline.Execute(new Context(s, e));
                });

            // btnSelectFile
            _eventBinder.BindCtrl(
                h => btnSelectFile.Click += h,
                h => btnSelectFile.Click -= h,
                (_, __) =>
                {
                    using (var ofd = new OpenFileDialog { Filter = _fileFilter })
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            txtSelectFile.Text = ofd.FileName;
                        }
                    }
                });

            // btnApply
            _eventBinder.BindCtrl(
                h => btnApply.Click += h,
                h => btnApply.Click -= h,
                (s, e) =>
                {
                    // オフセット
                    if (txtTargetOffset.Enabled)
                    {
                        string offsetStr = txtTargetOffset.Text;
                        if (string.IsNullOrEmpty(offsetStr))
                        {
                            MessageBox.Show(
                                "アドレスを入力してください。",
                                "",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        Offset = offsetStr.ParseStringToInt();
                    }

                    // データタイプ
                    if (cmbDataType.Enabled)
                    {
                        DataTypeIndex = cmbDataType.SelectedIndex;
                    }

                    // エントリー数
                    if (nudEntryCount.Enabled)
                    {
                        EntryCount = (int)nudEntryCount.Value;
                    }

                    // ファイルパス
                    if (txtSelectFile.Enabled)
                    {
                        string path = txtSelectFile.Text.Trim();
                        if (string.IsNullOrEmpty(path))
                        {
                            MessageBox.Show(
                                "ファイルを選択してください。",
                                "",
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                            return;
                        }

                        FilePath = path;
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                });
        }

        private void SetupOffsetInput(int? offset)
        {
            if (!offset.HasValue) return;

            lblTargetOffset.Enabled = true;
            txtTargetOffset.Enabled = true;
            txtTargetOffset.Text = offset.Value.ToString("X8");
        }

        private void SetupComboBox(string[] items)
        {
            if (items == null || items.Length == 0) return;

            lblDataType.Enabled = true;
            cmbDataType.Enabled = true;
            cmbDataType.Items.Clear();
            cmbDataType.Items.AddRange(items);
            cmbDataType.SelectedIndex = 0;
        }

        private void SetupNumericUpDown(decimal? min, decimal? max)
        {
            if (!min.HasValue || !max.HasValue) return;

            lblEntryCount.Enabled = true;
            nudEntryCount.Enabled = true;
            nudEntryCount.Minimum = min.Value;
            nudEntryCount.Maximum = max.Value;
            nudEntryCount.Value = min.Value;
        }

        private void SetupFileInput(string filter)
        {
            if (string.IsNullOrEmpty(filter)) return;

            lblSelectFile.Enabled = true;
            txtSelectFile.Enabled = true;
            btnSelectFile.Enabled = true;
        }
    }
}
