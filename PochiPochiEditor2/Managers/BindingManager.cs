using System;
using System.Collections.Generic;
using System.Windows.Forms;

using PochiPochiEditor2.Managers;
using PochiPochiEditor2.Managers.Fields;

namespace PochiPochiEditor2.Managers
{
    public class BindingManager : IDisposable
    {
        private  Control _parentCtrl;
        private TblManager _charmap;

        // 一括管理用
        private List<IDisposable> _bindings = new List<IDisposable>();

        /// <summary>
        /// 探索範囲と文字列変換を事前に準備する。
        /// </summary>
        public BindingManager(Control parentCtrl, TblManager charmap)
        {
            _parentCtrl = parentCtrl;
            _charmap = charmap;
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
                _charmap,
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
