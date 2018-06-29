using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Format_Chunk
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] ID = new byte[4]
        {
      (byte) 102,
      (byte) 109,
      (byte) 116,
      (byte) 32
        };
        public uint Size = 0U;
        public ushort FormatTag = (ushort)1;
        public ushort Channels = (ushort)1;
        public uint SamlesPerSec = 16000U;
        public uint AvgBytesPerSec = 0U;
        public ushort BlockAlign = (ushort)2;
        public ushort BitsPerSample = (ushort)16;
    }
}
