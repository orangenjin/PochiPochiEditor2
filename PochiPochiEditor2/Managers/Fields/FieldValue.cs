using System;
using System.Linq;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public string Name { get; }
        public int EntryLength { get; }
        public int AllowedLength { get; } // ほぼstring用
        public bool IsSigned { get; }
        public bool IsPointer { get; }
        public byte[] BinaryData { get; set; } // 可変長の場合の長さは.Lengthで取得
        public string[] ControlNames { get; set; } // 自動設定の後でも上書き可能

        // 変更検知用
        public event EventHandler DataUpdated = null;

        /// <summary>
        /// DefReaderで読み込んだ定義情報からコンテナを作成する。
        /// </summary>
        public FieldValue(
            FieldMetaData metaData, 
            SharedData sharedData,
            byte[] binaryData = null) // 後入れ可能
        {
            // フィールド名を格納
            Name = metaData.Name;

            // StringAttributeであるか確認
            var stringAttr = metaData.Attrs
                .FirstOrDefault(a => a.Kind == FieldExtensions.AttrKind.StringAttr);

            // 現状stringだけ動的長さを計算する必要あり
            if (stringAttr != null)
            {
                // 属性引数AllowedLengthがない場合がある
                int maxCount = Math.Min(
                    stringAttr.Args.Length, 
                    FieldExtensions.AttrKind.StringAttr.GetAttrSize());

                int[] lengths = new int[maxCount];
                for (int i = 0; i < maxCount; i++)
                {
                    lengths[i] = sharedData.Config.ReadInt(stringAttr.Args[i]);
                }

                // 存在しない場合も考慮、その場合同値を入れる
                EntryLength = lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
                AllowedLength = lengths.Length > 1
                    ? lengths[(int)FieldExtensions.StringAttrArgs.AllowedLengthArg]
                    : lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
            }
            else
            {
                EntryLength = metaData.Field.GetFieldSize();
                AllowedLength = Constants.InvalidValue;
            }

            // 符号ありかどうかを判定
            IsSigned = metaData.Field is FieldExtensions.FieldKind.SByte || 
                metaData.Field is FieldExtensions.FieldKind.Int16 ||
                metaData.Field is FieldExtensions.FieldKind.Int32;

            // ポインタかどうかを判定
            IsPointer = metaData.Field is FieldExtensions.FieldKind.Pointer;

            // コントロールと紐づけ
            if (metaData.Ctrl == FieldExtensions.CtrlKind.none)
            {
                // ない場合は紐づけを除外
                ControlNames = Array.Empty<string>();
            }
            else
            {
                // StringAttr以外
                var otherAttr = metaData.Attrs?
                    .FirstOrDefault(a => a.Kind != FieldExtensions.AttrKind.StringAttr);

                if (otherAttr != null) // 高々1つと仮定
                {
                    ControlNames = otherAttr.Args
                        .Select(arg => $"{metaData.Ctrl}{arg}")
                        .ToArray();
                }
                else
                {
                    // [コントロールのプレフィックス] + [フィールド名]
                    ControlNames = new string[] { $"{metaData.Ctrl}{metaData.Name}" };
                }
            }

            // 後入れ可能
            BinaryData = binaryData;
        }

        /// <summary>
        /// BinaryDataから型Tの値を取得する。indexはControlNamesに対応。
        /// </summary>
        public T GetData<T>(
            TblManager charmap, // 文字変換用
            Func<FieldValue, int, TblManager, T> converter = null,
            int ctrlNameindex = 0)
        {
            // 特殊処理があれば渡す
            return converter != null
                ? converter(this, ctrlNameindex, charmap)
                : CalcHelper.BytesToModelConv<T>(this, charmap, ctrlNameindex);
        }

        /// <summary>
        /// 型Tの値をBinaryDataに適用する。indexはControlNamesに対応。
        /// </summary>
        public void SetData<T>(
            T rawData,
            TblManager charmap, // 文字変換用
            Func<T, FieldValue, TblManager, int, byte[]> converter = null,
            int ctrlNameindex = 0)
        {
            // 特殊処理があれば渡す
            byte[] newBytes = converter != null
                    ? converter(rawData, this, charmap, ctrlNameindex)
                    : CalcHelper.ModelToBytesConv(rawData, this, charmap, ctrlNameindex);

            // 更新されたbyte[]を適用
            BinaryData = newBytes;

            // データが更新されたことを通知
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }


    }
}
