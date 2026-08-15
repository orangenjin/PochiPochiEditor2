using System;
using System.Collections.Generic;
using System.Windows.Forms;

using PochiPochiEditor2.Managers.Fields;

namespace PochiPochiEditor2.Managers
{
    public class BindingManager : IDisposable
    {
        private  Control _parentCtrl;

        // 一括管理用
        private List<IDisposable> _bindings = new List<IDisposable>();

        /// <summary>
        /// 探索範囲を指定する。
        /// </summary>
        public BindingManager(Control parentCtrl)
        {
            _parentCtrl = parentCtrl;
        }

        /// <summary>
        /// FieldBindingを登録する。
        /// </summary>
        public void AddBinding<T>(
            FieldValue fieldValue,
            Action<Control, T> customUiSetter = null,
            Func<Control, T> customUiGetter = null,
            Func<FieldValue, int, TblManager, T> customDataGetter = null,
            Func<T, FieldValue, TblManager, int, byte[]> customDataSetter = null,
            int ctrlNameIndex = 0)
        {
            // FieldValueからコントロール名を取得
            string controlName = fieldValue.ControlNames[ctrlNameIndex];
            var targetControls = _parentCtrl.Controls.Find(controlName, true);

            var binding = new FieldBinding<T>(
                targetControls[0],
                fieldValue,
                customUiSetter,
                customUiGetter,
                customDataGetter,
                customDataSetter,
                ctrlNameIndex);

            _bindings.Add(binding);
        }

        /// <summary>
        /// Listを破棄する。
        /// </summary>
        public void Dispose()
        {
            foreach (var binding in _bindings)
            {
                binding.Dispose();
            }
            _bindings.Clear();
        }
    }
}
