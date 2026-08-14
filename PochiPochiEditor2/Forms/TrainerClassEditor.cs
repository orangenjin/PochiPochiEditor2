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
        private readonly SharedData _sharedData;

        public TrainerClassEditor(SharedData sharedData)
        {
            InitializeComponent();

            _sharedData = sharedData;

            // test
            string defFileName = "TrainerClassPrizeMultiEntry";
            int tableOffset = _sharedData.Config.ReadInt("TrainerClassPrizeMultiTableOffset");
            int entrycount = _sharedData.Config.ReadInt("TrainerClassPrizeMultiCount");
            var entries = new EntryManager(defFileName, _sharedData, tableOffset, entrycount);

            txtClassName.Text = entries.Entries[0].Fields[0].GetData<byte>(sharedData).ToString();
        }
    }
}
