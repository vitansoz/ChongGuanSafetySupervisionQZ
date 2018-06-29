using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Data_Chunk
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ID = new byte[4]
        {
      (byte) 100,
      (byte) 97,
      (byte) 116,
      (byte) 97
        };
        public uint Size = 0U;
    }
}
