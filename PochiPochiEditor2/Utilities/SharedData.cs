using System;

using PochiPochiEditor2.Managers;

namespace PochiPochiEditor2.Utilities
{
    public class SharedData
    {
        public byte[] RomData { get; set; }　// 後入れ
        public bool IsRomLoaded { get; set; }
        public IniManager Config { get; }
        public TblManager Charmap { get; }

        public SharedData(
            IniManager config,
            TblManager charmap)
        {
            Config = config;
            Charmap = charmap;

            IsRomLoaded = false;
        }

        public void LoadRom(byte[] romData)
        {
            RomData = romData;
            IsRomLoaded = true;
        }

        public void ClearRom()
        {
            RomData = Array.Empty<byte>();
            IsRomLoaded = false;
        }
    }
}
