using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class Fact_Chunk
    {
        public byte[] ID = new byte[4]
        {
      (byte) 102,
      (byte) 97,
      (byte) 99,
      (byte) 116
        };
        public uint Size = 0U;
        public byte[] Temp;
    }
}
