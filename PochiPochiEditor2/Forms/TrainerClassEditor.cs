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
        private SharedData _sharedData = null;
        private EntryManager _className = null;


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


        }

        private void InitializeEntries()
        {
            // 肩書名テーブルを作成
            string defFileName = "TrainerClassNameEntry";
            int tableOffset = _sharedData.Config.ReadInt("TrainerClassNameTableOffset");
            int entrycount = _sharedData.Config.ReadInt("TrainerClassNameCount");
            _className = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);

            
        }





        // txtClassName.Text = _className.Entries[1][FieldKey.ClassName].GetData<string>(_sharedData);
    }
}
