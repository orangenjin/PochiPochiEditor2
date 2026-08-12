using System;
using System.IO;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;

namespace PochiPochiEditor2
{
    public partial class MainForm : Form
    {
        // ボタンのタグ用
        private enum SaveMode
        {
            Overwrite,
            SaveAs
        }

        // ファイルパス
        private string _romPath = string.Empty;
        private string _iniFolder = Path.Combine(Application.StartupPath, "ini");

        // フォーム同時起動用
        // private FormGroupManager _formGroupManager = null;
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();

        public MainForm()
        {
            InitializeComponent();
            InitializeEventHandlers();
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
                h => btnSaveAs.Click += h,
                h => btnSaveAs.Click -= h,
                btnSaveAs_Click);
            _eventBinder.BindCtrl(
                h => btnSaveOver.Click += h,
                h => btnSaveOver.Click -= h,
                btnSaveOver_Click);

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

        }

        private void btnClearRom_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveOver_Click(object sender, EventArgs e)
        {

        }

        private void EditorButton_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // shared data
            SharedData.Instance.Config = new IniManager(_iniFolder);
            SharedData.Instance.Charmap = new TblFileReader(_tblPath);

            // set tags
            btnOverwrite.Tag = SaveMode.Overwrite;
            btnSaveAs.Tag = SaveMode.SaveAs;

            MainFormUIUpdate();
        }
    }
}
