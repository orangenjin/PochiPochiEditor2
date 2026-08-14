namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldAttribute
    {
        public FieldExtensions.AttrKind Kind { get; }
        public string[] Args { get; }

        public FieldAttribute(FieldExtensions.AttrKind attrKind, params string[] args)
        {
            Kind = attrKind;
            Args = args;
        }
    }
}
