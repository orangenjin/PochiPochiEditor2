namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldAttribute
    {
        public AttributeKind Kind { get; }
        public string[] Parameters { get; }

        public FieldAttribute(AttributeKind attributeKind, params string[] parameters)
        {
            Kind = attributeKind;
            Parameters = parameters;
        }
    }

    public enum AttributeKind
    {
        StringAttribute,
        NibbleAttribute,
        BitAttribute
    }
}
