using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class CardClass
    {
        [DllImport("termb.dll")]
        public static extern int InitCommExt();

        [DllImport("termb.dll")]
        public static extern int CloseComm();

        [DllImport("termb.dll")]
        public static extern int Authenticate();

        [DllImport("termb.dll")]
        public static extern int Read_Content(int active);

        [DllImport("termb.dll")]
        public static extern int GetCardInfo(int index, StringBuilder value);

        [DllImport("termb.dll")]
        public static extern int GetBmpPhotoExt();

        [DllImport("termb.dll")]
        public static extern int GetBmpPhoto(string path);

        [DllImport("termb.dll")]
        public static extern void GetPhotoJPGPathName(StringBuilder path, int value);

        [DllImport("termb.dll")]
        public static extern void getJPGPhotoBase64(StringBuilder path, int value);

        [DllImport("termb.dll")]
        public static extern void SetCardJPGPathNameH(string path);

        [DllImport("termb.dll")]
        public static extern int ExportCardImageH();

        [DllImport("termb.dll")]
        public static extern int ExportCardImageF();
    }
}
