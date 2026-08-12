using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class FormGroupManager
    {
        private Form _ownerForm = null;
        private List<Form> _forms = null;

        // メイン画面のUI状態更新用
        public event EventHandler Closed = null;

        public FormGroupManager(Form ownerForm, FormGroup group, SharedData sharedData)
        {
            _ownerForm = ownerForm;
            _forms = new List<Form>();

            // グループ判定
            var formTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t == typeof(Form))
                .Where(t => t.GetCustomAttribute<FormGroupAttribute>().Group == group);

            // フォーム作成
            foreach (var type in formTypes)
            {
                var form = (Form)Activator.CreateInstance(type, sharedData);

                form.FormClosed += SingleForm_FormClosed;
                _forms.Add(form);
            }
        }

        public void ShowFormGroup()
        {
            foreach (var form in _forms)
            {
                form.Show(_ownerForm);
            }
        }

        private void SingleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var form in _forms)
            {
                if (ReferenceEquals(form, sender)) continue; // 既に閉じている

                if (!form.IsDisposed)
                {
                    form.FormClosed -= SingleForm_FormClosed;
                    form.Close();
                }
            }
            _forms.Clear();

            // 呼び出し元フォームを前に出す
            _ownerForm.BringToFront();
            Closed.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 属するフォームグループを指定する。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FormGroupAttribute : Attribute
    {
        public FormGroup Group { get; }

        public FormGroupAttribute(FormGroup group)
        {
            Group = group;
        }
    }

    /// <summary>
    /// フォームグループの種類を定義する。
    /// </summary>
    public enum FormGroup
    {
        TrainerClass,
        TrainerSprite
    }
}
