using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class HwClass
    {
        [DllImport("HWTablet.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort HWInit(ref HwClass.HWDeviceInfo pBaseInfo, ref int uPenMsg, IntPtr hWnd);

        [DllImport("HWTablet.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort HWGetSerial(ref HwClass.HWDeviceInfo pBaseInfo, ref int uPenMsg, IntPtr hWnd, ref HwClass.HWSignPad pSignPad);

        [DllImport("HWTablet.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort HWPacketsGet(int nMaxPkts, ref HwClass.HWPacket pkt);

        [DllImport("HWTablet.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort HWClearSig();

        [DllImport("HWTablet.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort HWClose(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWDeviceInfo
        {
            public ushort VendorID;
            public ushort ProductID;
            public int nXExt;
            public int nYExt;
            public int pressure;
            public int penState;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWPacket
        {
            public int nXPos;
            public int nYPos;
            public int nPress;
            public byte nPenType;
            public int nButton;
            public int nScroll;
            public int nTime;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWSignPad
        {
            public byte IDReport;
            public byte flag;
            public byte category;
            public ushort pid;
            public byte industry;
            public ushort serial;
        }
    }
}
