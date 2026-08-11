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

    // 右辺は最大パラメータ数
    public enum AttributeKind
    {
        StringAttribute = 2,

        // byte想定
        NibbleAttribute = 2,
        BitAttribute = 8
    }
}
