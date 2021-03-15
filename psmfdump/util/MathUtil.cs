using System;

namespace VGMToolbox.util
{
    public class MathUtil
    {
        public static long RoundUpToByteAlignment(long valueToRound, long byteAlignment)
        {
            long roundedValue = -1;

            roundedValue = (valueToRound + byteAlignment - 1) / byteAlignment * byteAlignment;

            return roundedValue;
        }

        public static ulong RoundUpToByteAlignment(ulong valueToRound, ulong byteAlignment)
        {
            ulong roundedValue;

            roundedValue = (valueToRound + byteAlignment - 1) / byteAlignment * byteAlignment;

            return roundedValue;
        }
    }

}
