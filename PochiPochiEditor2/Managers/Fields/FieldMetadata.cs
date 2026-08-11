using System;
using System.Collections.Generic;
using System.Linq;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public FieldKind Kind { get; }
        public List<FieldAttribute> Attributes { get; }

        public FieldMetaData(string name, FieldKind kind, List<FieldAttribute> attributes = null)
        {
            Name = name;
            Kind = kind;
            Attributes = attributes?.ToList();
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
}
