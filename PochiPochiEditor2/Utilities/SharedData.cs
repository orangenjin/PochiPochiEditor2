using System;

using PochiPochiEditor2.Managers;

namespace PochiPochiEditor2.Utilities
{
    public class SharedData
    {
        public byte[] RomData { get; set; }　// 後入れ
        public IniManager Config { get; set; }
        public TblManager Charmap { get; set; }

        public SharedData(
            IniManager config,
            TblManager charmap)
        {
            Config = config;
            Charmap = charmap;
        }

        public void ClearRom()
        {
            RomData = Array.Empty<byte>();
        }
    }
}
