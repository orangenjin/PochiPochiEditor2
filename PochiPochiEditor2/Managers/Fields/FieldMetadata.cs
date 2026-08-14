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

    /// <summary>
    /// 右辺はバイトサイズ。
    /// </summary>
    public enum FieldKind
    {
        Byte = 1,
        SByte = 1,
        UInt16 = 2,
        Int16 = 2,
        UInt32 = 4,
        Int32 = 4,
        Pointer = 4, // 特殊
        String = -1 // 適当
    }
}
