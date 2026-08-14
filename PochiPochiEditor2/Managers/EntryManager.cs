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


        public List<FieldValue> Fields { get; } = new List<FieldValue>();

        public EntryManager(
            string defFileName, 
            SharedData sharedData, 
            int tableOffset,
            int entryCount)
        {
            // DefReaderから定義情報を読み込む
            var defReader = new DefReader(defFileName);

            for (int i = 0; i < entryCount; i++)
            {
                // 内部位置
                int fieldOffset = 0;

                for (int j = 0; j < defReader.FieldDefs.Count; j++)
                {
                    // FieldValueを仮生成
                    var fieldValue = new FieldValue(
                        defReader.FieldDefs[j],
                        sharedData,
                        defReader.CtrlDefs[j]);

                    // バイナリデータを取得
                    fieldValue.BinaryData = new byte[fieldValue.EntryLength];
                    Array.Copy(
                        sharedData.RomData,
                        tableOffset,
                        fieldValue.BinaryData,
                        0,
                        fieldValue.EntryLength);

                    Fields.Add(fieldValue);
                    fieldOffset += fieldValue.EntryLength;
                }
            }
        }
    }
}
