using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PochiPochiEditor2.Managers.Fields
{
    public class FieldValue
    {
        public string Name { get; }
        public byte[] Value { get; set; }
        public int EntryLength { get; set; }
        public int AllowedLength { get; set; }

        public FieldValue(FieldMetadata metadata)
        {
            // フィールド名を格納
            Name = metadata.Name;

            // 長さを計算

        }
    }
}
