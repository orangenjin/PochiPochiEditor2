namespace PochiPochiEditor2.Managers.Entries
{
    public class FieldAttribute
    {
        public AttributeType AttributeType { get; }
        public object[] Parameters { get; }

        public FieldAttribute(AttributeType attributeType, params object[] parameters)
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
