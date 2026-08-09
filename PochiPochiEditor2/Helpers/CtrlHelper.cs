using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PochiPochiEditor2.Helpers
{
    public static class ControlHelper
    {
        /// <summary>
        /// コンテナを指定して、再帰的にコントロールを有効化/無効化する。
        /// </summary>
        public static void SetControlsEnabled(
            Control container,
            bool enabled,
            bool includeSelf = true,
            IEnumerable<string> excludeNames = null,
            IEnumerable<Type> excludeTypes = null)
        {
            foreach (var ctrl in GetTargetControls(container, includeSelf, excludeNames, excludeTypes))
            {
                ctrl.Enabled = enabled;
            }
        }

        /// <summary>
        /// コンテナを指定して、再帰的にコントロールを初期化する。
        /// </summary>
        public static void ResetControls(
            Control container,
            bool includeSelf = true,
            IEnumerable<string> excludeNames = null,
            IEnumerable<Type> excludeTypes = null)
        {
            foreach (var ctrl in GetTargetControls(container, includeSelf, excludeNames, excludeTypes))
            {
                // 種類が少ないから、直接記述している。
                switch (ctrl)
                {
                    case TextBox textBox:
                        textBox.Text = string.Empty;
                        break;
                    case NumericUpDown nud:
                        nud.Value = Math.Max(nud.Minimum, 0);
                        break;
                    case ComboBox comboBox:
                        comboBox.SelectedIndex = -1;
                        break;
                    case CheckBox checkBox:
                        checkBox.Checked = false;
                        break;
                    case RadioButton radioButton:
                        radioButton.Checked = false;
                        break;
                }
            }
        }

        /// <summary>
        /// 条件に合致するコントロールを返す。
        /// </summary>
        private static List<Control> GetTargetControls(
            Control container,
            bool includeSelf,
            IEnumerable<string> excludeNames,
            IEnumerable<Type> excludeTypes)
        {
            var results = new List<Control>();

            // 例外設定
            var nameSet = excludeNames?.ToList();
            var typeSet = excludeTypes?.ToList();

            // 自身を含むかどうか
            if (includeSelf && !IsExcluded(container))
            {
                results.Add(container);
            }

            // 探索して終了
            SearchCtrl(container);
            return results;

            // 除外対象かどうかを判定するヘルパー
            bool IsExcluded(Control ctrl) =>
                (nameSet?.Contains(ctrl.Name) ?? false) ||
                (typeSet?.Contains(ctrl.GetType()) ?? false);

            // 再帰的に探すためにメソッド化
            void SearchCtrl(Control parent)
            {
                // コンテナであるか判定
                if (!ShouldRecurse(parent)) return;

                foreach (Control child in parent.Controls)
                {
                    if (!IsExcluded(child))
                    {
                        results.Add(child);
                    }

                    SearchCtrl(child);
                }
            }
        }

        /// <summary>
        /// 変な挙動をしないように、一応再帰すべきコンテナを指定する。
        /// </summary>
        public static bool ShouldRecurse(Control ctrl)
        {
            return ctrl is Form ||
                   ctrl is Panel ||
                   ctrl is GroupBox ||
                   ctrl is TabControl ||
                   ctrl is TabPage;
        }

        /// <summary>
        /// テキスト自動整形機能を追加する。
        /// </summary>
        public static void AttachAutoFormat(int digits = 8, params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                _showDigits[textBox] = digits; // 桁数を仮置き

                textBox.Leave -= FormatTextBox;
                textBox.Leave += FormatTextBox;
            }
        }

        /// <summary>
        /// テキスト自動整形機能を解除する。
        /// </summary>
        public static void DetachAutoFormat(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.Leave -= FormatTextBox;
                _showDigits.Remove(textBox);
            }
        }

        // コントロール別の整形桁数を保持
        private static readonly Dictionary<TextBox, int> _showDigits = new Dictionary<TextBox, int>();

        private static void FormatTextBox(object sender, EventArgs e)
        {
            // 型変換を兼ねる
            if (!(sender is TextBox textBox)) return;

            // 空白ならそのまま
            if (string.IsNullOrWhiteSpace(textBox.Text)) return;

            // 一応トリミング
            var trimmedText = textBox.Text.Trim();

            // 小文字の "null" に統一する
            if (trimmedText.Equals(Constants.InvalidOffsetString, StringComparison.OrdinalIgnoreCase))
            {
                textBox.Text = Constants.InvalidOffsetString;
                return;
            }

            // 変換テスト
            if (CalcHelper.TryParseOffset(textBox.Text.Trim(), out int resultValue))
            {
                // tagの桁数を参照
                int digits = _showDigits[textBox];

                textBox.Text = resultValue.ToString($"X{digits}");
            }
            else
            {
                textBox.Text = string.Empty; // 空白にする
            }
        }
    }
}
