namespace PochiPochiEditor2.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// defファイル行において、コロンで区切られる順番。
        /// </summary>
        public enum DefName
        {
            FieldName,
            KindName,
            AttrName,
            CtrlName
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
            BitAttr,

            // flag想定（特殊）
            FlagAttr
        }

        /// <summary>
        /// AttrKindに対応するサイズを取得する。
        /// </summary>
        public static int GetAttrSize(this AttrKind kind)
        {
            switch (kind)
            {
                case AttrKind.StringAttr:
                    return 2; // EntryLength, AllowedLength

                case AttrKind.NibbleAttr:
                    return Constants.CharPerByte;

                case AttrKind.BitAttr:
                    return Constants.BitsPerByte;

                default:
                    return Constants.InvalidValue;
            }
        }
    }
}
