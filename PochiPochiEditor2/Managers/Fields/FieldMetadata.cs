using System;
using System.Collections.Generic;
using System.Linq;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetadata
    {
        public string Name { get; }
        public FieldKind Kind { get; }
        public List<FieldAttribute> Attributes { get; }

        public FieldMetadata(string name, FieldKind kind, List<FieldAttribute> attributes = null)
        {
            Name = name;
            Kind = kind;
            Attributes = attributes?.ToList();
        }

        // サイズ取得用
        public int Length
        {
            get
            {
                switch (Kind)
                {
                    case FieldKind.Byte:
                        return sizeof(byte);
                    case FieldKind.SByte:
                        return sizeof(sbyte);
                    case FieldKind.UInt16:
                        return sizeof(ushort);
                    case FieldKind.Int16:
                        return sizeof(short);
                    case FieldKind.UInt32:
                        return sizeof(uint);
                    case FieldKind.Int32:
                        return sizeof(int);
                    case FieldKind.String:


                        return sizeof(int);
                    default:
                        return default;
                }
            }
        }

        // 符号ありかどうかを判定
        public bool IsSigned =>
            Kind is FieldKind.SByte || Kind is FieldKind.Int16 || Kind is FieldKind.Int32;
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
}
