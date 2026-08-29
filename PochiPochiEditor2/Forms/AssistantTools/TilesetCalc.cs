using System;
using System.Drawing;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Forms.AssistantTools
{
    public partial class TilesetCalc : Form
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // イベント登録・解除用
        private TilesetManager _tilesetManager = null;

        public TilesetCalc(SharedData sharedData)
        {
            InitializeComponent();

            _tilesetManager = new TilesetManager(sharedData);
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            _eventBinder.BindCtrl(
                h => btnToOffset.Click += h,
                h => btnToOffset.Click -= h,
                BtnToOffset_Click);
            _eventBinder.BindCtrl(
                h => btnToNo.Click += h,
                h => btnToNo.Click -= h,
                BtnToNo_Click);

            // 解除タイミング指定
            _eventBinder.BindCtrl(
                h => this.Disposed += h,
                h => this.Disposed -= h);
        }

        private void BtnToOffset_Click(object sender, EventArgs e)
        {
            int tilesetNo = (int)nudTilesetNo.Value;
            int offset = _tilesetManager.CalcOffset(tilesetNo);

            txtHeaderOffset.Text = offset.ParseIntToString();

            SetSuccess();
        }

        private void BtnToNo_Click(object sender, EventArgs e)
        {
            int offset = txtHeaderOffset.Text.ParseStringToInt();

            // 空白、16進数出ない場合
            if (offset == Constants.InvalidValue)
            {
                SetFailure(Constants.DefaultIndex);
                return;
            }

            if (_tilesetManager.TryCalcTilesetNo(offset, out int tilesetNo))
            {
                nudTilesetNo.Value = tilesetNo;
                SetSuccess();
            }
            else
            {
                // 近い番号を計算
                int recNo = _tilesetManager.CalcNearestTilesetNo(offset);
                SetFailure(recNo);
            }
        }

        private void SetSuccess()
        {
            // 成功時のUI更新
            lblResult.Text = "成功";
            lblResult.ForeColor = Color.Green;

            // 推奨値をリセット
            CtrlHelper.ResetControls(grpResult, includeSelf: false);
        }

        private void SetFailure(int recNo)
        {
            // 失敗時のUI更新
            lblResult.Text = "失敗";
            lblResult.ForeColor = Color.Red;

            // 推奨番号とオフセットの表示
            nudRecNo.Value = recNo;
            txtRecOffset.Text = _tilesetManager.CalcOffset(recNo).ParseIntToString();
        }
    }
}
