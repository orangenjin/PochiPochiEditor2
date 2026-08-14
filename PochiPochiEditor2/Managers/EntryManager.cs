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
            byte[] romData,
            int baseOffset,
            ControlKind[] controlKinds)
        {
            // DefReaderから定義情報を読み込む
            var defReader = new DefReader(defFileName);

            for (int i = 0; i < defReader.FieldDefs.Count; i++)
            {
                // FieldValueを仮生成
                var fieldValue = new FieldValue(
                    defReader.FieldDefs[i], 
                    sharedData, 
                    controlKinds[i]);

                // バイナリデータを取得
                fieldValue.BinaryData = new byte[fieldValue.EntryLength];
                Array.Copy(romData, baseOffset, fieldValue.BinaryData, 0, fieldValue.EntryLength);

                Fields.Add(fieldValue);
                baseOffset += fieldValue.EntryLength;

                


            }
        }
    }
}
