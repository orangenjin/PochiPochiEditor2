using System.Collections.Generic;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public Extensions.FieldKind Field { get; }
        public Extensions.CtrlKind Ctrl { get; }
        public List<FieldAttribute> Attrs { get; }

        public FieldMetaData(
            string name,
            Extensions.FieldKind kind,
            Extensions.CtrlKind ctrl,
            List<FieldAttribute> attrs)
        {
            Name = name;
            Field = kind;
            Ctrl = ctrl;
            Attrs = attrs;
        }
    }
}
