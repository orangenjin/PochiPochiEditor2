using System.Collections.Generic;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public FieldExtensions.FieldKind Field { get; }
        public FieldExtensions.CtrlKind Ctrl { get; }
        public List<FieldAttribute> Attrs { get; }

        public FieldMetaData(
            string name,
            FieldExtensions.FieldKind kind,
            FieldExtensions.CtrlKind ctrl,
            List<FieldAttribute> attrs)
        {
            Name = name;
            Field = kind;
            Ctrl = ctrl;
            Attrs = attrs;
        }
    }
}
