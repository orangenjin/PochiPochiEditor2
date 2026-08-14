using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class EntryManager
    {
        public List<Entry> Entries { get; set; }

        public EntryManager(
            string defFileName, 
            SharedData sharedData,
            int tableOffset, 
            int entryCount)
        {
            // 初期化
            Entries = new List<Entry>();

            // DefReaderから定義情報を読み込む
            var defReader = new DefReader(defFileName);

            // カーソル用
            int currentOffset = tableOffset;

            for (int i = 0; i < entryCount; i++)
            {
                var entryFields = new List<FieldValue>();

                for (int j = 0; j < defReader.FieldDefs.Count; j++)
                {
                    // FieldValueを仮生成
                    var fieldValue = new FieldValue(
                        defReader.FieldDefs[j],
                        sharedData);

                    // バイナリデータを代入
                    fieldValue.BinaryData = new byte[fieldValue.EntryLength];
                    Array.Copy(
                        sharedData.RomData,
                        currentOffset,
                        fieldValue.BinaryData,
                        0,
                        fieldValue.EntryLength);

                    entryFields.Add(fieldValue);
                    currentOffset += fieldValue.EntryLength;
                }

                Entries.Add(new Entry(entryFields));
            }
        }
    }

    public class Entry
    {
        public List<FieldValue> Fields { get; set; }

        public Entry(List<FieldValue> fields)
        {
            Fields = new List<FieldValue>();
            Fields.AddRange(fields);
        }

        // 単一エントリーのサイズ
        public int EntrySize => Fields.Sum(f => f.EntryLength);
    }
}
