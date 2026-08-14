using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PochiPochiEditor2.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// defファイル行において、コロンで区切られる順番。
        /// </summary>
        public enum DefPosition
        {
            FieldName,
            KindName,
            AttributeName,
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
        public static int GetByteSize(this FieldKind kind)
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
                    return Constants.InvalidSize;
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
    }
}
