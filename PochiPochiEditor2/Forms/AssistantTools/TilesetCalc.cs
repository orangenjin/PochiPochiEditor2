using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Forms.AssistantTools
{
    public partial class TilesetCalc : Form
    {
        // イベント登録・解除用
        private EventBinder _eventBinder = new EventBinder();
        // 共有データ用
        private SharedData _sharedData = null;

        private int _baseOffset = default;
        private int _entryLength = default;

        private enum FieldKey
        {
            ImageCompType,
            PaletteType,
            TilesetHeaderUnk1,
            TilesetHeaderUnk2,
            ImageOffset,
            PaletteOffset,
            BlockArg1Offset,
            AnimDataOffset,
            BlockArg2Offset
        }

        private static class DefName
        {
            public static string TilesetHeaderEntry = nameof(TilesetHeaderEntry);
        }

        private static class IniKey
        {
            public static string TilesetHeaderBaseOffset = nameof(TilesetHeaderBaseOffset);
        }

        public TilesetCalc(SharedData sharedData)
        {
            InitializeComponent();
            _sharedData = sharedData;

            _baseOffset = _sharedData.Config.ReadInt(IniKey.TilesetHeaderBaseOffset);

            // エントリーサイズを求める
            var tilesetHeaderDef = new DefReader(DefName.TilesetHeaderEntry);
            var entryFields = new List<FieldValue>();
            for (int i = 0; i < tilesetHeaderDef.FieldDefs.Count; i++)
            {
                // FieldValueを生成
                var fieldValue = new FieldValue(
                    _sharedData,
                    tilesetHeaderDef.FieldDefs[i],
                    typeof(FieldKey));

                entryFields.Add(fieldValue);
            }
            _entryLength = entryFields.Sum(f => f.EntryLength);

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
            int offset = _baseOffset + (tilesetNo * _entryLength);
            txtHeaderOffset.Text = offset.ParseIntToString();

            SetSuccess();
        }

        private void BtnToNo_Click(object sender, EventArgs e)
        {
            try
            {
                // 16進数文字列を数値に変換
                int offset = txtHeaderOffset.Text.ParseStringToInt();

                // ベースオフセット未満の場合は失敗として
                if (offset < _baseOffset)
                {
                    SetFailure(Constants.DefaultIndex);
                    return;
                }

                int diff = offset - _baseOffset;
                int remainder = diff % _entryLength;
                int exactNo = diff / _entryLength;

                if (remainder == 0)
                {
                    // 完全一致
                    nudTilesetNo.Value = exactNo;
                    SetSuccess();
                }
                else
                {
                    int recNo = exactNo + 1;
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
            int recOffset = _baseOffset + (recNo * _entryLength);
            txtRecOffset.Text = recOffset.ParseIntToString();
        }
    }
}
