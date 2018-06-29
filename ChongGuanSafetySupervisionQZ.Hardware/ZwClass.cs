using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class ZwClass
    {
        [DllImport("ZKFPModule.dll")]
        public static extern IntPtr ZKFPModule_Connect(string lpParams);

        [DllImport("ZKFPModule.dll")]
        public static extern int ZKFPModule_EnrollUserByScan(IntPtr Handle, int nUserID);

        [DllImport("ZKFPModule.dll")]
        public static extern int ZKFPModule_GetFingerImage(IntPtr Handle, ref int width, ref int heigth, byte[] imgData, ref int dataSize);

        [DllImport("ZKFPModule.dll")]
        public static extern int ZKFPModule_Disconnect(IntPtr Handle);

        [DllImport("ZKFPModule.dll")]
        public static extern int ZKFPModule_ClearDB(IntPtr Handle);
    }
}
