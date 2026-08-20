using System;
using System.Collections.Generic;
using System.Linq;

using PochiPochiEditor2.Managers.Fields;
using PochiPochiEditor2.Utilities;

namespace PochiPochiEditor2.Managers
{
    public class EntryManager
    {
        public List<Entry> Entries { get; }
        public int BaseOffset { get; }

        public EntryManager(
            string defFileName,
            Type enumType,
            SharedData sharedData,
            int tableOffset, 
            int entryCount)
        {
            // 初期化
            Entries = new List<Entry>();

            // DefReaderから定義情報を読み込む
            var defReader = new DefReader(defFileName);

            // 後の書き込み用に保持
            BaseOffset = tableOffset;

            // 現在のエントリーのインデックス
            for (int i = 0; i < entryCount; i++)
            {
                //　単一エントリーに対するフィールド
                var entryFields = new List<FieldValue>();

                for (int j = 0; j < defReader.FieldDefs.Count; j++)
                {
                    // FieldValueを生成
                    var fieldValue = new FieldValue(
                        sharedData,
                        defReader.FieldDefs[j],
                        enumType);

                    entryFields.Add(fieldValue);
                }

                Entries.Add(new Entry(i, BaseOffset, entryFields));
            }
        }
    }

    public class Entry
    {
        // アクセス用
        private Dictionary<Enum, FieldValue> _fieldMap = null;

        // 自身のインデックスを保持
        public int EntryIndex { get; set; }
        public List<FieldValue> Fields { get; set; }

        public Entry(int index, int baseOffset, List<FieldValue> fields)
        {
            EntryIndex = index;
            Fields = fields;

            // 辞書化
            _fieldMap = Fields.ToDictionary(f => f.Name);

            // エントリーのオフセット
            int entryStartOffset = baseOffset + (EntryIndex * EntrySize);

            // 各フィールドのオフセットを計算
            int currentRelativeOffset = 0;
            foreach (var field in Fields)
            {
                field.RomOffset = entryStartOffset + currentRelativeOffset;
                currentRelativeOffset += field.EntryLength;
            }
        }

        /// <summary>
        /// 単一エントリーのサイズを計算する。
        /// </summary>
        public int EntrySize => Fields.Sum(f => f.EntryLength);

        /// <summary>
        /// FieldValueにアクセスするためのインデクサ。
        /// </summary>
        public FieldValue this[Enum fieldName]
        {
            get => _fieldMap[fieldName];
        }
    }
}
