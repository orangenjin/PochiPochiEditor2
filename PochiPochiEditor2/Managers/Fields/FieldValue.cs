using System;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Utilities;
namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public Enum Name { get; }
        public int EntryLength { get; }
        public int AllowedLength { get; } // string用
        public bool IsSigned { get; }
        public bool IsPointer { get; }
        public int ValueCount { get; } // nibble, bit用
        public int RomOffset { get; set; }
        public byte[] BinaryData
        {
            get
            {
                // RomDataから現在のデータを取得
                byte[] data = new byte[EntryLength];
                Array.Copy(_sharedData.RomData, RomOffset, data, Constants.DefaultIndex, EntryLength);
                return data;
            }
            set
            {
                // RomDataへ書き込み
                Array.Copy(value, Constants.DefaultIndex, _sharedData.RomData, RomOffset, EntryLength);
            }
        }

        // 共有データ用
        private SharedData _sharedData = null;

        /// <summary>
        /// DefReaderで読み込んだ定義情報からコンテナを作成する。
        /// </summary>
        public FieldValue(
            SharedData sharedData,
            FieldMetaData metaData,
            Type enumType)
        {
            // 後に使用するので保持
            _sharedData = sharedData;

            // フィールド名を変換
            Name = (Enum)Enum.Parse(enumType, metaData.Name);

            // 属性を確認、仮入れ
            ValueCount = default;
            EntryLength = metaData.Field.GetFieldSize();
            AllowedLength = Constants.InvalidValue;

            // ない場合は実行されない
            foreach (var attr in metaData.Attrs)
            {
                switch (attr.Kind)
                {
                    case FieldExtensions.AttrKind.StringAttr:
                        ValueCount = FieldExtensions.AttrKind.StringAttr.GetAttrSize(); // 一応

                        // 属性引数AllowedLengthがない場合がある
                        int maxCount = Math.Min(
                            attr.Args.Length,
                            FieldExtensions.AttrKind.StringAttr.GetAttrSize());

                        int[] lengths = new int[maxCount];
                        for (int i = 0; i < maxCount; i++)
                        {
                            lengths[i] = sharedData.Config.ReadInt(attr.Args[i]);
                        }

                        // 存在しない場合、同値を入れる
                        EntryLength = lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
                        AllowedLength = lengths.Length > 1
                            ? lengths[(int)FieldExtensions.StringAttrArgs.AllowedLengthArg]
                            : lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
                        break;

                    case FieldExtensions.AttrKind.NibbleAttr:
                        ValueCount = FieldExtensions.AttrKind.NibbleAttr.GetAttrSize();
                        break;

                    case FieldExtensions.AttrKind.BitAttr:
                        ValueCount = FieldExtensions.AttrKind.BitAttr.GetAttrSize();
                        break;
                }
            }

            // 符号ありかどうかを判定
            IsSigned = metaData.Field is FieldExtensions.FieldKind.SByte || 
                metaData.Field is FieldExtensions.FieldKind.Int16 ||
                metaData.Field is FieldExtensions.FieldKind.Int32;

            // ポインタかどうかを判定
            IsPointer = metaData.Field is FieldExtensions.FieldKind.Pointer;
        }

        /// <summary>
        /// BinaryDataから型Tの値を取得する。
        /// </summary>
        public T GetData<T>(
            int valueindex = Constants.DefaultIndex,
            Func<FieldValue, int, TblManager, T> converter = null)
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
            int valueindex = Constants.DefaultIndex,
            Func<T, FieldValue, int, TblManager, byte[]> converter = null)
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
