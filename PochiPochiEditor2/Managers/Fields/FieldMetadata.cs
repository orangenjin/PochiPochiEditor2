using System.Collections.Generic;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public Extensions.FieldKind Field { get; }
        public List<FieldAttribute> Attrs { get; }
        public Extensions.CtrlKind Ctrl { get; }


        public FieldMetaData(
            string name,
            Extensions.FieldKind kind,
            List<FieldAttribute> attrs,
            Extensions.CtrlKind ctrl)
        {
            Name = name;
            Field = kind;
            Attrs = attrs;
            Ctrl = ctrl;
        }
    }
}
