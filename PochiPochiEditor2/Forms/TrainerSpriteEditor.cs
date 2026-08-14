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
    [FormGroup(FormGroup.TrainerSprite)]
    public partial class TrainerSpriteEditor : Form
    {
        private SharedData _sharedData = null;

        public TrainerSpriteEditor(SharedData sharedData)
        {
            InitializeComponent();

            _sharedData = sharedData;

            // test

        }
    }
}
