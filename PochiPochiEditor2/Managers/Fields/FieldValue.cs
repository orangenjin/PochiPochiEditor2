using System;
using System.Linq;

using PochiPochiEditor2.Helpers;
using PochiPochiEditor2.Managers.Commands;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public Enum Name { get; }
        public int EntryLength { get; } // 最大長として
        public int AllowedLength { get; } // ほぼstring用
        public bool IsSigned { get; }
        public bool IsPointer { get; }
        public int ArgCount { get; } // ほぼnibble, bit用
        public int Offset { get; set; } // 読み書き用
        public byte[] BinaryData // SharedData.RomDataと紐づいている
        {
            get
            {
                // RomDataから現在のデータを取得
                byte[] data = new byte[EntryLength];
                Array.Copy(_sharedData.RomData, Offset, data, Constants.DefaultIndex, EntryLength);
                return data;
            }
            set
            {
                // RomDataへ書き込み
                Array.Copy(value, Constants.DefaultIndex, _sharedData.RomData, Offset, EntryLength);
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
            ArgCount = default;
            EntryLength = metaData.Field.GetFieldSize();
            AllowedLength = Constants.InvalidValue;

            // ない場合は実行されない（要素数0を仮置きしている）
            foreach (var attr in metaData.Attrs)
            {
                // 場合分け後、属性引数の数を格納
                switch (attr.Kind)
                {
                    case FieldExtensions.AttrKind.StringAttr:
                        // 属性引数AllowedLengthがない場合がある
                        ArgCount = Math.Min(
                            attr.Args.Length, 
                            FieldExtensions.AttrKind.StringAttr.GetAttrCount()); // 最大値と比較

                        // 長さintを格納する
                        int[] lengths = new int[ArgCount];
                        for (int i = 0; i < ArgCount; i++)
                        {
                            lengths[i] = sharedData.Config.ReadInt(attr.Args[i]);
                        }

                        // AllowedLengthが存在しない場合、同値を入れる
                        EntryLength = lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
                        AllowedLength = lengths.Length > 1
                            ? lengths[(int)FieldExtensions.StringAttrArgs.AllowedLengthArg]
                            : lengths[(int)FieldExtensions.StringAttrArgs.EntryLengthArg];
                        break;

                    case FieldExtensions.AttrKind.NibbleAttr:
                        ArgCount = FieldExtensions.AttrKind.NibbleAttr.GetAttrCount();
                        break;

                    case FieldExtensions.AttrKind.BitAttr:
                        ArgCount = FieldExtensions.AttrKind.BitAttr.GetAttrCount();
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
            int argIndex = Constants.DefaultIndex,
            Func<FieldValue, int, TblManager, T> converter = null)
        {
            // 通常の型Tで対応できない特殊処理があれば渡す
            return converter != null
                ? converter(this, argIndex, _sharedData.Charmap)
                : CalcHelper.BytesToModelConv<T>(this, argIndex, _sharedData.Charmap);
        }

        /// <summary>
        /// 型Tの値をBinaryDataに適用する。
        /// </summary>
        public void SetData<T>(
            T rawData,
            int argIndex = Constants.DefaultIndex,
            Func<T, FieldValue, int, TblManager, byte[]> converter = null)
        {
            // 通常の型Tで対応できない特殊処理があれば渡す
            byte[] newBytes = converter != null
                    ? converter(rawData, this, argIndex, _sharedData.Charmap)
                    : CalcHelper.ModelToBytesConv(rawData, this, argIndex, _sharedData.Charmap);

            // 新しいbyte[]を代入
            BinaryData = newBytes;
        }

        /// <summary>
        /// 簡易的に値(通常)を更新する。
        /// </summary>
        public void UpdateData<T>(
            UndoManager undoManager,
            T data,
            string desc,
            int argIndex = Constants.DefaultIndex)
        {
            var command = CreateUpdateCommand(
                data,
                desc,
                argIndex);

            if (command != null)
            {
                undoManager.PushCommand(command);
            }
        }

        /// <summary>
        /// コマンドを生成する。
        /// </summary>
        public ICommand CreateUpdateCommand<T>(
            T data,
            string desc,
            int argIndex = Constants.DefaultIndex)
        {
            // 変更前のバイナリデータ
            byte[] oldBinary = BinaryData;
            // データ更新
            SetData(data, argIndex);
            // 変更後のバイナリデータ
            byte[] newBinary = BinaryData;

            // 同じなら無視
            if (oldBinary.SequenceEqual(newBinary)) return null;

            return new FieldChangeCommand(
                this,
                oldBinary,
                newBinary,
                desc);
        }
    }
}
