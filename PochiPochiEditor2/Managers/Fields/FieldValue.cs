using System;
using System.Linq;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Utilities;
namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public Enum Name { get; }
        public int EntryLength { get; }
        public int AllowedLength { get; } // ほぼstring用
        public bool IsSigned { get; }
        public bool IsPointer { get; }
        public byte[] BinaryData { get; set; } // 後入れ

        // 共有データ用
        private SharedData _sharedData = null;

        /// <summary>
        /// DefReaderで読み込んだ定義情報からコンテナを作成する。
        /// </summary>
        public FieldValue(
            SharedData sharedData,
            FieldMetaData metaData, 
            Enum fieldKey,
            byte[] binaryData = null)
        {
            // 後に使用するので保持
            _sharedData = sharedData;

            // フィールド名を変換
            Type enumType = fieldKey.GetType();
            Name = (Enum)Enum.Parse(enumType, metaData.Name);

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

                // 存在しない場合、同値を入れる
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

            // 後入れ可能
            BinaryData = binaryData;
        }

        /// <summary>
        /// BinaryDataから型Tの値を取得する。
        /// </summary>
        public T GetData<T>(
            Func<FieldValue, int, TblManager, T> converter = null,
            int valueindex = 0)
        {
            // 特殊処理があれば渡す
            return converter != null
                ? converter(this, valueindex, _sharedData.Charmap)
                : CalcHelper.BytesToModelConv<T>(this, valueindex, _sharedData.Charmap);
        }

        /// <summary>
        /// 型Tの値をBinaryDataに適用する。
        /// </summary>
        public void SetData<T>(
            T rawData,
            Func<T, FieldValue, int, TblManager, byte[]> converter = null,
            int valueindex = 0)
        {
            // 特殊処理があれば渡す
            byte[] newBytes = converter != null
                    ? converter(rawData, this, valueindex, _sharedData.Charmap)
                    : CalcHelper.ModelToBytesConv(rawData, this, valueindex, _sharedData.Charmap);

            // 新しいbyte[]を代入
            BinaryData = newBytes;
        }
    }
}
