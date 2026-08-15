using System;

using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public static class FieldExtensions
    {
        /// <summary>
        /// defファイル行において、コロンで区切られる順番。
        /// </summary>
        public enum DefName
        {
            FieldName,
            KindName,
            CtrlName,
            AttrName
        }

        /// <summary>
        /// defファイルのフィールドの種類を定義する。
        /// </summary>

        public enum FieldKind
        {
            Byte,
            SByte,
            UInt16,
            Int16,
            UInt32,
            Int32,
            Pointer,
            String
        }

        /// <summary>
        /// FieldKindに対応するサイズを取得する。
        /// </summary>
        public static int GetFieldSize(this FieldKind kind)
        {
            switch (kind)
            {
                case FieldKind.Byte:
                case FieldKind.SByte:
                    return Constants.ByteSize;

                case FieldKind.UInt16:
                case FieldKind.Int16:
                    return Constants.UShortSize;

                case FieldKind.UInt32:
                case FieldKind.Int32:
                case FieldKind.Pointer:
                    return Constants.UIntSize;
                
                // stringは動的長さ
                default:
                    return Constants.InvalidValue;
            }
        }

        /// <summary>
        /// defファイルのバインドできるコントロールを定義する。
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

        /// <summary>
        /// defファイルの属性の種類を定義する。
        /// </summary>
        public enum AttrKind
        {
            StringAttr,

            // byte想定
            NibbleAttr,
            BitAttr
        }

        public enum StringAttrArgs
        {
            EntryLengthArg,
            AllowedLengthArg
        }

        public enum NibbleAttrArgs
        {
            HighValueArg,
            LowValueArg
        }

        public enum BitAttrArgs
        {
            Bit0Arg,
            Bit1Arg,
            Bit2Arg,
            Bit3Arg,
            Bit4Arg,
            Bit5Arg,
            Bit6Arg,
            Bit7Arg
        }

        /// <summary>
        /// AttrKindに対応する引数の数を取得する。
        /// </summary>
        public static int GetAttrSize(this AttrKind kind)
        {
            // 戻り値用
            int count = default;

            switch (kind)
            {
                case AttrKind.StringAttr:
                    count = Enum.GetValues(typeof(StringAttrArgs)).Length;
                    break;

                case AttrKind.NibbleAttr:
                    count = Enum.GetValues(typeof(NibbleAttrArgs)).Length;
                    break;

                case AttrKind.BitAttr:
                    count = Enum.GetValues(typeof(BitAttrArgs)).Length;
                    break;
            }

            return count;
        }
    }
}
