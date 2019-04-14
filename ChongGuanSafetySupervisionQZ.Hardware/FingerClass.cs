using libzkfpcsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class FingerClass
    {
        public static int Init()
        {
            int num = -1;
            try
            {
                num = zkfp2.Init();
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static int Terminate()
        {
            int num = -1;
            try
            {
                num = zkfp2.Terminate();
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static int CloseDevice(IntPtr devHandle)
        {
            int num = -1;
            try
            {
                num = zkfp2.CloseDevice(devHandle);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static IntPtr OpenDevice(int index)
        {
            IntPtr num = IntPtr.Zero;
            try
            {
                num = zkfp2.OpenDevice(index);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static int AcquireFingerprint(IntPtr devHandle, byte[] imgBuffer, byte[] template, ref int size)
        {
            int num = -1;
            try
            {
                num = zkfp2.AcquireFingerprint(devHandle, imgBuffer, template, ref size);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static int GetParameters(IntPtr devHandle, int code, byte[] paramValue, ref int size)
        {
            int num = -1;
            try
            {
                num = zkfp2.GetParameters(devHandle, code, paramValue, ref size);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public static bool ByteArray2Int(byte[] buf, ref int value)
        {
            bool flag = false;
            try
            {
                flag = zkfp2.ByteArray2Int(buf, ref value);
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
