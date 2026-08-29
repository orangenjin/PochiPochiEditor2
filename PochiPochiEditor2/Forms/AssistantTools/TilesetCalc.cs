using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            int offset = CalcOffsetFromTilesetNo(tilesetNo);

            txtHeaderOffset.Text = offset.ParseIntToString();

            SetSuccess();
        }

        private void BtnToNo_Click(object sender, EventArgs e)
        {
            try
            {
                int offset = txtHeaderOffset.Text.ParseStringToInt();

                if (TryCalcTilesetNoFromOffset(offset, out int tilesetNo))
                {
                    nudTilesetNo.Value = tilesetNo;
                    SetSuccess();
                }
                else
                {
                    int diff = offset - _tilesetManager.BaseOffset;
                    int recNo = diff < 0
                        ? Constants.DefaultIndex
                        : (diff / _tilesetManager.EntryLength) + 1;

                    SetFailure(recNo);
                }
            }
            catch
            {
                SetFailure(Constants.DefaultIndex);
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
            int recOffset = _tilesetManager.BaseOffset + (recNo * _tilesetManager.EntryLength);
            txtRecOffset.Text = recOffset.ParseIntToString();
        }

        /// <summary>
        /// タイルセット番号からヘッダーオフセットを計算する。
        /// </summary>
        public int CalcOffsetFromTilesetNo(int tilesetNo)
        {
            return _tilesetManager.BaseOffset + (tilesetNo * _tilesetManager.EntryLength);
        }

        /// <summary>
        /// ヘッダーオフセットからタイルセット番号を計算する。
        /// 完全一致しない場合は失敗する。
        /// </summary>
        public bool TryCalcTilesetNoFromOffset(int offset, out int tilesetNo)
        {
            tilesetNo = Constants.InvalidValue;

            // ベースオフセットより小さい場合
            if (offset < _tilesetManager.BaseOffset) return false;

            int diff = offset - _tilesetManager.BaseOffset;

            // 完全一致でない場合
            if (diff % _tilesetManager.EntryLength != 0) return false;

            tilesetNo = diff / _tilesetManager.EntryLength;
            return true;
        }
    }
}
