using System;
using System.Windows.Forms;

namespace PochiPochiEditor2.Managers.Fields
{
    /// <summary>
    /// 一組のUIとFieldValueの双方向同期を管理する。
    /// </summary>
    public class FieldBinding<T> : IDisposable
    {
        // 必須引数
        private Control _control = null;
        private FieldValue _fieldValue = null;

        // 手動設定用
        private Action<Control, T> _uiSetter = null;
        private Func<Control, T> _uiGetter = null;
        private Func<FieldValue, int, TblManager, T> _dataGetter = null;
        private Func<T, FieldValue, TblManager, int, byte[]> _dataSetter = null;
        private int _ctrlNameIndex = 0;

        // 無限ループ防止用
        private bool _isSynchronizing = false;

        public FieldBinding(
            Control control,
            FieldValue fieldValue,
            Action<Control, T> customUiSetter = null,
            Func<Control, T> customUiGetter = null,
            Func<FieldValue, int, TblManager, T> customDataGetter = null,
            Func<T, FieldValue, TblManager, int, byte[]> customDataSetter = null,
            int ctrlNameIndex = 0)
        {
            _control = control;
            _fieldValue = fieldValue;

            _uiSetter = customUiSetter;
            _uiGetter = customUiGetter;
            _dataGetter = customDataGetter;
            _dataSetter = customDataSetter;
            _ctrlNameIndex = ctrlNameIndex;

            // コントロールに入れる（Disposeで解除）
            AttachControlEvent();

            // FieldValueに入れる（Disposeで解除）
            _fieldValue.DataUpdated += OnDataUpdated;

            // 初期化（一度実行する）
            UpdateUIFromData();
        }

        /// <summary>
        /// FieldValueが変更された時、UIを更新する。
        /// </summary>
        private void OnDataUpdated(object sender, EventArgs e)
        {
            UpdateUIFromData();
        }

        private void UpdateUIFromData()
        {
            // 無限ループ阻止
            if (_isSynchronizing) return;
            _isSynchronizing = true;

            try
            {
                // FieldValueからGetDataで型Tとして値を取得
                T value = _fieldValue.GetData(_dataGetter, _ctrlNameIndex);

                // 特殊処理があれば優先する
                if (_uiSetter != null)
                {
                    _uiSetter(_control, value);
                }
                else
                {
                    ApplyValueToControl(value);
                }
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        /// <summary>
        /// UIが変更された時にFieldValueを更新する。
        /// </summary>
        private void OnControlValueChanged(object sender, EventArgs e)
        {
            // 無限ループ阻止
            if (_isSynchronizing) return;
            _isSynchronizing = true;

            try
            {
                // コントロールから値を取得
                T value = _uiGetter != null 
                    ? _uiGetter(_control) 
                    : ExtractValueFromControl();

                // FieldValueからSetDataで更新
                _fieldValue.SetData(value, _dataSetter, _ctrlNameIndex);
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        /// <summary>
        /// 対応するイベントを選択する。
        /// </summary>
        private void AttachControlEvent()
        {
            switch (_control)
            {
                // CtrlKind.txt
                case TextBox txt: 
                    txt.Leave += OnControlValueChanged; // 暫定Leave
                    break;

                // CtrlKind.nud
                case NumericUpDown nud: 
                    nud.ValueChanged += OnControlValueChanged; 
                    break;

                // CtrlKind.chk
                case CheckBox chk: 
                    chk.CheckedChanged += OnControlValueChanged; 
                    break;

                // CtrlKind.cmb
                case ComboBox cmb: 
                    cmb.SelectedIndexChanged += OnControlValueChanged; 
                    break;

                // CtrlKind.rb
                case RadioButton rb: 
                    rb.CheckedChanged += OnControlValueChanged; 
                    break;

                default: 
                    _control.Validated += OnControlValueChanged; // 適当
                    break;
            }
        }

        private void DetachControlEvent()
        {
            switch (_control)
            {
                case TextBox txt:
                    txt.Leave -= OnControlValueChanged;
                    break;

                case NumericUpDown nud:
                    nud.ValueChanged -= OnControlValueChanged;
                    break;

                case CheckBox chk:
                    chk.CheckedChanged -= OnControlValueChanged;
                    break;

                case ComboBox cmb:
                    cmb.SelectedIndexChanged -= OnControlValueChanged;
                    break;

                case RadioButton rb:
                    rb.CheckedChanged -= OnControlValueChanged;
                    break;

                default:
                    _control.Validated -= OnControlValueChanged;
                    break;
            }
        }

        private void ApplyValueToControl(T value)
        {
            switch (_control)
            {
                case TextBox txt: 
                    txt.Text = value?.ToString() ?? ""; // 他の処理で8桁整形あり
                    break;
                case NumericUpDown nud:
                    nud.Value = Convert.ToDecimal(value); 
                    break;
                case CheckBox chk: 
                    chk.Checked = Convert.ToBoolean(value);
                    break;
                case ComboBox cmb:
                    cmb.SelectedIndex = Convert.ToInt32(value);
                    break;
                case RadioButton rb: 
                    rb.Checked = Convert.ToBoolean(value); 
                    break;
            }
        }

        private T ExtractValueFromControl()
        {
            object rawValue = null;
            switch (_control)
            {
                case TextBox txt: 
                    rawValue = txt.Text;
                    break;
                case NumericUpDown nud: 
                    rawValue = nud.Value; 
                    break;
                case CheckBox chk: 
                    rawValue = chk.Checked; 
                    break;
                case ComboBox cmb: 
                    rawValue = cmb.SelectedIndex; 
                    break;
                case RadioButton rb: 
                    rawValue = rb.Checked;
                    break;
            }
            return (T)Convert.ChangeType(rawValue, typeof(T));
        }

        public void Dispose()
        {
            _fieldValue.DataUpdated -= OnDataUpdated;
            DetachControlEvent();
        }
    }
}
