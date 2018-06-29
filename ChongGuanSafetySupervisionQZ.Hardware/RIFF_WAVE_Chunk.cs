using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RIFF_WAVE_Chunk
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] szRiffID = new byte[4]
        {
      (byte) 82,
      (byte) 73,
      (byte) 70,
      (byte) 70
        };
        public uint dwRiffSize = 0U;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] szRiffFormat = new byte[4]
        {
      (byte) 87,
      (byte) 65,
      (byte) 86,
      (byte) 69
        };
    }
}
