using System;
using System.IO;
using System.Windows.Forms;

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

        private FormGroupManager _formGroupManager = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            

            // shared data
            SharedData.Instance.Config = new IniFileReader(_iniFolder, cmbConfig);
            SharedData.Instance.Charmap = new TblFileReader(_tblPath);

            // set tags
            btnOverwrite.Tag = SaveMode.Overwrite;
            btnSaveAs.Tag = SaveMode.SaveAs;

            MainFormUIUpdate();
        }
    }
}
