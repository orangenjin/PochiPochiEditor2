using System.Collections.Generic;
using System.Linq;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldMetaData
    {
        public string Name { get; }
        public FieldKind Field { get; }
        public CtrlKind Ctrl { get; }
        public List<FieldAttribute> Attributes { get; }

        public FieldMetaData(
            string name, 
            FieldKind kind,
            CtrlKind ctrl, 
            List<FieldAttribute> attributes)
        {
            Name = name;
            Field = kind;
            Ctrl = ctrl;
            Attributes = attributes;
        }
    }

    /// <summary>
    /// コロンで区切られた順番。
    /// </summary>
    public enum DefPosition
    {
        FieldName,
        KindName,
        CtrlName,
        AttributeName
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

    /// <summary>
    /// バインドできるコントロールの定義をする。
    /// </summary>
    public enum CtrlKind
    {
        none,
        txt,
        nud,
        cmb,
        chk,
        rb
    }
}
