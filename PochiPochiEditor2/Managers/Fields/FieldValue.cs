using System;
using System.Linq;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public string Name { get; }
        public int EntryLength { get; }
        public int AllowedLength { get; } // ほぼstring用
        public bool IsSigned { get; }
        public byte[] BinaryData { get; set; } // 可変長の場合のValueの長さは.Lengthで

        // DefReaderで読み込んだ定義情報から作成
        public FieldValue(FieldMetaData metaData, SharedData sharedData)
        {
            // フィールド名を格納
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

                // 存在しない場合も考慮
                EntryLength = lengths[0];
                AllowedLength = lengths.Length > 1
                    ? lengths[1] :
                    -1;
            }
            else
            {
                EntryLength = (int)metaData.Kind;
                AllowedLength = -1;
            }

            // 符号ありかどうかを判定
            IsSigned = metaData.Kind is FieldKind.SByte || 
                metaData.Kind is FieldKind.Int16 ||
                metaData.Kind is FieldKind.Int32;
        }

        public void SetData<T>(T rawData)
        {
            // T の型から自動的に処理を選択

            // 自動選択されたテンプレートでセット

            // むしろ事前に詰め込むほうがよいか？
        }

        public void SetData<T>(T rawData, Func<T, byte[]> processor)
        {
        }
    }
}
