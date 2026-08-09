using System;
using System.Collections.Generic;

namespace PochiPochiEditor2.Mangers.Entry
{
    public class FieldDefinition
    {
        public class FieldMetadata
        {
            public string Name { get; set; }
            public FieldKind Kind { get; set; }
            public List<FieldAttribute> Attributes { get; set; }

            public FieldMetadata(string name, FieldKind kind)
            {
                Name = name;
                Kind = kind;
                Attributes = new List<FieldAttribute>();
            }

            // Type取得用
            public Type Type
            {
                get
                {
                    switch (Kind)
                    {
                        case FieldKind.Byte:
                            return typeof(byte);
                        case FieldKind.SByte:
                            return typeof(sbyte);
                        case FieldKind.UInt16:
                            return typeof(ushort);
                        case FieldKind.Int16:
                            return typeof(short);
                        case FieldKind.UInt32:
                            return typeof(uint);
                        case FieldKind.Int32:
                            return typeof(int);
                        default:
                            return typeof(string); // FieldKind.String
                    }
                }
            }

            // 符号ありかどうかを判定
            public bool IsSigned =>
                Kind is FieldKind.SByte || Kind is FieldKind.Int16 || Kind is FieldKind.Int32;
        }

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
    }

    public enum FieldKind
    {
        Byte,
        SByte,
        UInt16,
        Int16,
        UInt32,
        Int32,
        String
    }

    public enum AttributeType
    {
        StringAttribute,
        NibbleAttribute,
        BitAttribute
    }
}
