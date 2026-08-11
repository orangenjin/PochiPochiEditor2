using PochiPochiEditor2.Managers;

namespace PochiPochiEditor2.Utilities
{
    public class SharedData
    {
        public byte[] RomData { get; set; }
        public IniManager Config { get; set; }
        public TblManager Charmap { get; set; }

        public SharedData(
            byte[] romData,
            IniManager config,
            TblManager charmap)
        {
            RomData = romData;
            Config = config;
            Charmap = charmap;
        }
    }
}
