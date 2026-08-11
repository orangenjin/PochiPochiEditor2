using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public string Name { get; }
        public int EntryLength { get; }
        public int AllowedLength { get; } // string用
        public byte[] BinaryData { get; set; } // 可変長の場合のValueの長さは.Lengthで

        public FieldValue(FieldMetaData metaData, SharedData sharedData)
        {
            // フィールド名を格納
            Name = metaData.Name;

            // 長さを計算
            foreach (var attribute in metaData.Attributes)
            {
                // 現状stringだけ動的長さなので
                if(attribute.Kind != AttributeKind.StringAttribute) continue;

                // 要素数
                int maxCount = Math.Min(attribute.Parameters.Length, (int)AttributeKind.StringAttribute);

                int[] lengths = new int[maxCount];
                for (int i = 0; i < maxCount; i++)
                {
                    lengths[i] = sharedData.Config.ReadInt(attribute.Parameters[i]);
                }

                // 存在しない場合も考慮
                EntryLength = lengths[0];
                AllowedLength = lengths.Length > 1 
                    ? lengths[1] :
                    -1;
            }
        }

        public void SetData<T>(T rawData)
        {
            // T の型から自動的に処理を選択

            // 自動選択されたテンプレートでセット
        }

        public void SetData<T>(T rawData, Func<T, byte[]> processor)
        {
        }
    }
}
