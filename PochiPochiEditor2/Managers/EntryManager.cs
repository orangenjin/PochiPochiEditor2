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
            Type enumType,
            SharedData sharedData,
            int baseOffset, 
            int entryCount)
        {
            // 初期化
            Entries = new List<Entry>();

            // DefReaderから定義情報を読み込む
            var defReader = new DefReader(defFileName);

            // 現在のエントリーのインデックスについて
            for (int i = 0; i < entryCount; i++)
            {
                //　単一エントリーに対するフィールドリスト
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

                Entries.Add(new Entry(baseOffset, i,  entryFields));
            }
        }
    }

    public class Entry
    {
        // 簡易アクセス用
        private Dictionary<Enum, FieldValue> _fieldMap = null;

        // 自身のインデックスを保持
        public int EntryIndex { get; set; }
        public List<FieldValue> Fields { get; set; }

        public Entry(int baseOffset, int index,  List<FieldValue> fields)
        {
            EntryIndex = index;
            Fields = fields;

            // 辞書化
            _fieldMap = Fields.ToDictionary(f => f.Name);

            // 現在のエントリーのオフセットを計算
            int entryStartOffset = baseOffset + (EntryIndex * EntrySize);

            // 各フィールドのオフセットを格納
            int currentRelativeOffset = 0;
            foreach (var field in Fields)
            {
                field.Offset = entryStartOffset + currentRelativeOffset;
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
