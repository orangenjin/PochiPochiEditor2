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

            // カーソル用
            int currentOffset = tableOffset;

            // 現在のエントリーのインデックス
            for (int i = 0; i < entryCount; i++)
            {
                //　単一エントリーに対するフィールド
                var entryFields = new List<FieldValue>();

                for (int j = 0; j < defReader.FieldDefs.Count; j++)
                {
                    // FieldValueを仮生成
                    var fieldValue = new FieldValue(
                        sharedData,
                        defReader.FieldDefs[j],
                        enumType);

                    // バイナリデータを代入
                    fieldValue.BinaryData = new byte[fieldValue.EntryLength];
                    Array.Copy(
                        sharedData.RomData,
                        currentOffset,
                        fieldValue.BinaryData,
                        Constants.DefaultIndex,
                        fieldValue.EntryLength);

                    entryFields.Add(fieldValue);
                    currentOffset += fieldValue.EntryLength;
                }

                Entries.Add(new Entry(i, entryFields));
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

        public Entry(int index, List<FieldValue> fields)
        {
            EntryIndex = index;
            Fields = fields;

            // 辞書化
            _fieldMap = Fields.ToDictionary(f => f.Name);
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
