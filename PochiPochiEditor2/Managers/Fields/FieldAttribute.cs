using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldAttribute
    {
        public Extensions.AttrKind Kind { get; }
        public string[] Args { get; }

        public FieldAttribute(Extensions.AttrKind attrKind, params string[] args)
        {
            Kind = attrKind;
            Args = args;
        }
    }
}
