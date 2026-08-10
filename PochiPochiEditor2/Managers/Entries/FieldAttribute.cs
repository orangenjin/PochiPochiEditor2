namespace PochiPochiEditor2.Managers.Entries
{
    public class FieldAttribute
    {
        public AttributeType Type { get; }
        public string[] Parameters { get; }

        public FieldAttribute(AttributeType attributeType, params string[] parameters)
        {
            Type = attributeType;
            Parameters = parameters;
        }
    }

    public enum AttributeType
    {
        StringAttribute,
        NibbleAttribute,
        BitAttribute
    }
}
