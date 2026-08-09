namespace PochiPochiEditor2.Managers.Entries
{
    public class FieldAttribute
    {
        public AttributeType AttributeType { get; }
        public string[] Parameters { get; }

        public FieldAttribute(AttributeType attributeType, params string[] parameters)
        {
            AttributeType = attributeType;
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
