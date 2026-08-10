using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PochiPochiEditor2.Managers.Entries
{
    public class EntryLoader
    {
        // フィールド名をキーとして、値を保持する
        private readonly Dictionary<string, IField> _fields;
        public IReadOnlyDictionary<string, IField> Fields => _fields;

        // フィールド一覧
        public List<FieldMetadata> Layouts { get; }

        public EntryLoader(List<FieldMetadata> layouts)
        {
            // 情報として一応保持
            Layouts = layouts;

            // メタデータに基づいて型安全な Field<T> インスタンスを事前に組み立てる
            foreach (var layout in Layouts)
            {
                var field = FieldFactory.CreateField(meta);
                _fields[meta.Name] = field;
            }
        }

        // EntryName[FieldName]というアクセスを可能にするため
        public object this[string fieldName]
        {
            get
            {
                // 失敗するとnullを返すので注意
                return _dictValues.TryGetValue(fieldName, out var val) 
                    ? val 
                    : null;
            }

            set
            {
                _dictValues[fieldName] = value;
            }
        }
    }
}
