using System;
using System.Collections.Generic;
using System.Linq;

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

        private Dictionary<string, FieldValue> _fieldMap;

        public Entry(List<FieldValue> fields)
        {
            Fields = fields;

            // 辞書化
            _fieldMap = Fields.ToDictionary(f => f.Name);
        }

        // 単一エントリーのサイズ
        public int EntrySize => Fields.Sum(f => f.EntryLength);

        /// <summary>
        /// FieldValueにアクセスするためのインデクサ。
        /// </summary>
        public FieldValue this[Enum key]
        {
            get
            {
                string fieldName = key.ToString();
                return _fieldMap[fieldName];
            }
        }
    }
}
