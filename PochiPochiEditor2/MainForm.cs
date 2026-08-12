using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2
{
    public partial class MainForm : Form
    {
        // ボタンのタグ用
        private enum SaveMode
        {
            SaveOver,
            SaveAs
        }

        // ファイルパス
        private string _romPath = string.Empty;
        private string _iniFolder = Path.Combine(Application.StartupPath, "ini");
        private string _tblPath = Path.Combine(Application.StartupPath, "charmap.tbl");
        private string _imagePath = Path.Combine(Application.StartupPath, "img", "poochyena.png");

        // フォーム同時起動用
        private FormGroupManager _formGroupManager = null;
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeEventHandlers();

            // 先にこれらを初期化（設定名のコンボボックスの初期化が必要）
            var config = new IniManager(_iniFolder, cmbConfig);
            var charmap = new TblManager(_tblPath);
            _sharedData = new SharedData(config, charmap);

            // タグ付加
            btnSaveOver.Tag = SaveMode.SaveOver;
            btnSaveAs.Tag = SaveMode.SaveAs;

            // 画像表示
            picPoke.Image = Image.FromFile(_imagePath);

            // UI状態の更新
            MainFormUIUpdate();
        }

        private void InitializeEventHandlers()
        {
            // 読み込み関連
            _eventBinder.BindCtrl(
                h => btnSelectRom.Click += h,
                h => btnSelectRom.Click -= h,
                btnSelectRom_Click);
            _eventBinder.BindCtrl(
                h => btnClearRom.Click += h,
                h => btnClearRom.Click -= h,
                btnClearRom_Click);

            // 保存関連
            _eventBinder.BindCtrl(
                h => btnSaveOver.Click += h,
                h => btnSaveOver.Click -= h,
                SaveButton_Click);
            _eventBinder.BindCtrl(
                h => btnSaveAs.Click += h,
                h => btnSaveAs.Click -= h,
                SaveButton_Click);

            // 各エディタ用
            foreach (Button btn in grpSelectEditor.Controls)
            {
                _eventBinder.BindCtrl(
                    h => btn.Click += h,
                    h => btn.Click -= h,
                    EditorButton_Click);
            }

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h
            );
        }

        private void btnSelectRom_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Constants.RomFileFilter;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // RomDataを更新
                    _romPath = openFileDialog.FileName;
                    var romData = File.ReadAllBytes(_romPath);
                    _sharedData.LoadRom(romData);

                    // 特定の設定名を読み込み
                    string selectedConfig = cmbConfig.SelectedItem.ToString();
                    _sharedData.Config.LoadConfig(selectedConfig, _sharedData.RomData);

                    // UIの状態を更新
                    MainFormUIUpdate();
                }
            }
        }

        private void btnClearRom_Click(object sender, EventArgs e)
        {
            // Rom情報を更新
            _romPath = string.Empty;
            _sharedData.ClearRom();

            // UIの状態を更新
            MainFormUIUpdate();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn) || !(btn.Tag is SaveMode mode)) return;

            if (mode == SaveMode.SaveOver) // 上書き
            {
                SaveRom(_romPath);
            }
            else if (mode == SaveMode.SaveAs) // 名前を付けて保存
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = Constants.RomFileFilter;
                    saveFileDialog.FileName = Path.GetFileName(_romPath);

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveRom(saveFileDialog.FileName);
                    }
                }
            }

            // 保存処理
            void SaveRom(string path)
            {
                try
                {
                    File.WriteAllBytes(path, _sharedData.RomData);
                    MessageBox.Show(
                        "保存に成功しました。",
                        "保存完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "保存エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;

            // "btn" を外す
            string groupName = button.Name.Substring(Constants.ButtonPrefix.Length);

            // グループ名を取得
            if (!Enum.TryParse(groupName, out FormGroup group)) return;

            // フォーム生成
            _formGroupManager = new FormGroupManager(this, group, _sharedData);
            _formGroupManager.Closed += (_, __) =>
            {
                _formGroupManager = null;
                MainFormUIUpdate();
            };
            _formGroupManager.ShowFormGroup();

            MainFormUIUpdate();
        }

        private void MainFormUIUpdate()
        {
            // 現在の状態を整理
            bool isRomLoaded = _sharedData.IsRomLoaded;
            bool isEditorOpen = _formGroupManager != null;

            // 読み込み前、エディタ起動前
            bool canLoadConfig = !isRomLoaded && !isEditorOpen;
            CtrlHelper.SetControlsEnabled(grpLoadRom, canLoadConfig, includeSelf: false, new[] { nameof(btnClearRom) });

            // 読み込み後、エディタ起動前
            bool canOpenEditor = isRomLoaded && !isEditorOpen;
            btnClearRom.Enabled = canOpenEditor;
            CtrlHelper.SetControlsEnabled(grpSelectEditor, canOpenEditor);
            CtrlHelper.SetControlsEnabled(grpSaveRom, canOpenEditor);

            // 読み込み後、エディタ起動後
            CtrlHelper.SetControlsEnabled(grpHistory, isRomLoaded);
            CtrlHelper.SetControlsEnabled(grpAssistantTool, isRomLoaded);
        }
    }
}
