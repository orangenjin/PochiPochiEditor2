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
        public byte[] BinaryData { get; set; } // 可変長の場合のValueの長さは.Lengthで取得
        public string[] ControlNames { get; set; } // 自動設定の後でも上書き可能

        // DefReaderで読み込んだ定義情報からコンテナ作成
        public FieldValue(
            FieldMetaData metaData, 
            SharedData sharedData,
            byte[] binaryData = null)
        {
            // FieldValueを利用する先で、扱いやすく加工する
            Name = metaData.Name;

            // StringAttributeを確認
            var stringAttr = metaData.Attributes
                .FirstOrDefault(a => a.Kind == AttributeKind.StringAttribute);

            // 現状stringだけ動的長さ
            if (stringAttr != null)
            {
                // 要素数
                int maxCount = Math.Min(stringAttr.Parameters.Length, (int)AttributeKind.StringAttribute);

                int[] lengths = new int[maxCount];
                for (int i = 0; i < maxCount; i++)
                {
                    lengths[i] = sharedData.Config.ReadInt(stringAttr.Parameters[i]);
                }

                // 存在しない場合も考慮、その場合同値
                EntryLength = lengths[0];
                AllowedLength = lengths.Length > 1
                    ? lengths[1]
                    : lengths[0];
            }
            else
            {
                EntryLength = (int)metaData.Field;
                AllowedLength = -1;
            }

            // 符号ありかどうかを判定
            IsSigned = metaData.Field is FieldKind.SByte || 
                metaData.Field is FieldKind.Int16 ||
                metaData.Field is FieldKind.Int32;

            // ポインタかどうかを判定
            IsPointer = metaData.Field is FieldKind.Pointer;

            // コントロールと紐づけ
            if (metaData.Ctrl == CtrlKind.none)
            {
                // 紐づけを除外
                ControlNames = Array.Empty<string>();
            }
            else
            {
                var otherAttribute = metaData.Attributes?
                    .FirstOrDefault(a => a.Kind != AttributeKind.StringAttribute);

                if (otherAttribute != null) // 高々1つと仮定
                {
                    ControlNames = otherAttribute.Parameters
                        .Select(param => $"{metaData.Ctrl}{param}")
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
        public T GetData<T>(SharedData sharedData, Func<SharedData, FieldValue, int, T> converter = null, int index = 0)
        {
            // 特殊処理があれば渡して
            if (converter != null)
            {
                return converter(sharedData, this, index);
            }

            // 通常変換
            return CalcHelper.BytesToModelConv<T>(sharedData, this, index);
        }

        public void SetData<T>(T rawData)
        {

        }

        public void SetData<T>(T rawData, Func<T, byte[]> processor)
        {

        }


    }
}
