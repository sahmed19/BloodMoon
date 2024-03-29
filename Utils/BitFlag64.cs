namespace BloodMoon.Utils
{
    public struct BitFlag64
    {
        public static readonly BitFlag64 zero = new BitFlag64();
        public static readonly BitFlag64 one = new BitFlag64(ulong.MaxValue);
        ulong bits;

        public BitFlag64(ulong b = 0)
        {
            bits = b;
        }

        public void Set(int index, bool val)
        {
            if (index < 0 || index > 63) throw new System.Exception($"Bit index {index} must be 0 < i < 64");

            ulong bit = (ulong)1 << index;
            if (val) bits = bits | bit;
            else bits = bits & ~bit;
        }
        public bool Get(int index)
        {
            if (index < 0 || index > 63) throw new System.Exception($"Bit index {index} must be 0 < i < 64");
            return (bits & (ulong)1 << index) == 1;
        }
    }
}