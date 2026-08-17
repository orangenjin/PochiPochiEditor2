using System.Collections.Generic;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public FieldExtensions.FieldKind Field { get; }
        public List<FieldAttribute> Attrs { get; }

        public FieldMetaData(
            string name,
            FieldExtensions.FieldKind kind,
            List<FieldAttribute> attrs)
        {
            Name = name;
            Field = kind;
            Attrs = attrs;
        }
    }

    public class FieldAttribute
    {
        public FieldExtensions.AttrKind Kind { get; }
        public string[] Args { get; }

        public FieldAttribute(
            FieldExtensions.AttrKind attrKind, 
            params string[] args)
        {
            Kind = attrKind;
            Args = args;
        }
    }
}
