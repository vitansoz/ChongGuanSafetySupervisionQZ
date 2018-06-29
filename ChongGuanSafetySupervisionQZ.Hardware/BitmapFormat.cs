using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    internal class BitmapFormat
    {
        public static void RotatePic(byte[] BmpBuf, int width, int height, ref byte[] ResBuf)
        {
            int num1 = width * height;
            try
            {
                int num2 = 0;
                while (num2 < num1)
                {
                    for (int index = 0; index < width; ++index)
                        ResBuf[num2 + index] = BmpBuf[num1 - num2 - width + index];
                    num2 += width;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static byte[] StructToBytes(object StructObj, int Size)
        {
            int length = Marshal.SizeOf(StructObj);
            byte[] destination = new byte[length];
            try
            {
                IntPtr num = Marshal.AllocHGlobal(length);
                Marshal.StructureToPtr(StructObj, num, false);
                Marshal.Copy(num, destination, 0, length);
                Marshal.FreeHGlobal(num);
                if (Size != 14)
                    return destination;
                byte[] numArray = new byte[Size];
                int index1 = 0;
                for (int index2 = 0; index2 < length; ++index2)
                {
                    if (index2 != 2 && index2 != 3)
                    {
                        numArray[index1] = destination[index2];
                        ++index1;
                    }
                }
                return numArray;
            }
            catch (Exception ex)
            {
                return destination;
            }
        }

        public static void GetBitmap(byte[] buffer, int nWidth, int nHeight, ref MemoryStream ms)
        {
            ushort num1 = (ushort)8;
            int length = 256;
            byte[] ResBuf = new byte[nWidth * nHeight * 2];
            try
            {
                BitmapFormat.BITMAPFILEHEADER bitmapfileheader = new BitmapFormat.BITMAPFILEHEADER();
                BitmapFormat.BITMAPINFOHEADER bitmapinfoheader = new BitmapFormat.BITMAPINFOHEADER();
                BitmapFormat.MASK[] maskArray = new BitmapFormat.MASK[length];
                int num2 = (nWidth + 3) / 4 * 4;
                bitmapinfoheader.biSize = Marshal.SizeOf((object)bitmapinfoheader);
                bitmapinfoheader.biWidth = nWidth;
                bitmapinfoheader.biHeight = nHeight;
                bitmapinfoheader.biPlanes = (ushort)1;
                bitmapinfoheader.biBitCount = num1;
                bitmapinfoheader.biCompression = 0;
                bitmapinfoheader.biSizeImage = 0;
                bitmapinfoheader.biXPelsPerMeter = 0;
                bitmapinfoheader.biYPelsPerMeter = 0;
                bitmapinfoheader.biClrUsed = length;
                bitmapinfoheader.biClrImportant = length;
                bitmapfileheader.bfType = (ushort)19778;
                bitmapfileheader.bfOffBits = 14 + Marshal.SizeOf((object)bitmapinfoheader) + bitmapinfoheader.biClrUsed * 4;
                bitmapfileheader.bfSize = bitmapfileheader.bfOffBits + (num2 * (int)bitmapinfoheader.biBitCount + 31) / 32 * 4 * bitmapinfoheader.biHeight;
                bitmapfileheader.bfReserved1 = (ushort)0;
                bitmapfileheader.bfReserved2 = (ushort)0;
                ms.Write(BitmapFormat.StructToBytes((object)bitmapfileheader, 14), 0, 14);
                ms.Write(BitmapFormat.StructToBytes((object)bitmapinfoheader, Marshal.SizeOf((object)bitmapinfoheader)), 0, Marshal.SizeOf((object)bitmapinfoheader));
                for (int index = 0; index < length; ++index)
                {
                    maskArray[index].redmask = (byte)index;
                    maskArray[index].greenmask = (byte)index;
                    maskArray[index].bluemask = (byte)index;
                    maskArray[index].rgbReserved = (byte)0;
                    ms.Write(BitmapFormat.StructToBytes((object)maskArray[index], Marshal.SizeOf((object)maskArray[index])), 0, Marshal.SizeOf((object)maskArray[index]));
                }
                BitmapFormat.RotatePic(buffer, nWidth, nHeight, ref ResBuf);
                byte[] numArray = (byte[])null;
                if (num2 - nWidth > 0)
                    numArray = new byte[num2 - nWidth];
                for (int index = 0; index < nHeight; ++index)
                {
                    ms.Write(ResBuf, index * nWidth, nWidth);
                    if (num2 - nWidth > 0)
                        ms.Write(ResBuf, 0, num2 - nWidth);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteBitmap(byte[] buffer, int nWidth, int nHeight)
        {
            ushort num1 = (ushort)8;
            int length = 256;
            byte[] ResBuf = new byte[nWidth * nHeight];
            try
            {
                BitmapFormat.BITMAPFILEHEADER bitmapfileheader = new BitmapFormat.BITMAPFILEHEADER();
                BitmapFormat.BITMAPINFOHEADER bitmapinfoheader = new BitmapFormat.BITMAPINFOHEADER();
                BitmapFormat.MASK[] maskArray = new BitmapFormat.MASK[length];
                int num2 = (nWidth + 3) / 4 * 4;
                bitmapinfoheader.biSize = Marshal.SizeOf((object)bitmapinfoheader);
                bitmapinfoheader.biWidth = nWidth;
                bitmapinfoheader.biHeight = nHeight;
                bitmapinfoheader.biPlanes = (ushort)1;
                bitmapinfoheader.biBitCount = num1;
                bitmapinfoheader.biCompression = 0;
                bitmapinfoheader.biSizeImage = 0;
                bitmapinfoheader.biXPelsPerMeter = 0;
                bitmapinfoheader.biYPelsPerMeter = 0;
                bitmapinfoheader.biClrUsed = length;
                bitmapinfoheader.biClrImportant = length;
                bitmapfileheader.bfType = (ushort)19778;
                bitmapfileheader.bfOffBits = 14 + Marshal.SizeOf((object)bitmapinfoheader) + bitmapinfoheader.biClrUsed * 4;
                bitmapfileheader.bfSize = bitmapfileheader.bfOffBits + (num2 * (int)bitmapinfoheader.biBitCount + 31) / 32 * 4 * bitmapinfoheader.biHeight;
                bitmapfileheader.bfReserved1 = (ushort)0;
                bitmapfileheader.bfReserved2 = (ushort)0;
                Stream output = (Stream)File.Open("finger.bmp", FileMode.Create, FileAccess.Write);
                BinaryWriter binaryWriter = new BinaryWriter(output);
                binaryWriter.Write(BitmapFormat.StructToBytes((object)bitmapfileheader, 14));
                binaryWriter.Write(BitmapFormat.StructToBytes((object)bitmapinfoheader, Marshal.SizeOf((object)bitmapinfoheader)));
                for (int index = 0; index < length; ++index)
                {
                    maskArray[index].redmask = (byte)index;
                    maskArray[index].greenmask = (byte)index;
                    maskArray[index].bluemask = (byte)index;
                    maskArray[index].rgbReserved = (byte)0;
                    binaryWriter.Write(BitmapFormat.StructToBytes((object)maskArray[index], Marshal.SizeOf((object)maskArray[index])));
                }
                BitmapFormat.RotatePic(buffer, nWidth, nHeight, ref ResBuf);
                byte[] numArray = (byte[])null;
                if (num2 - nWidth > 0)
                    numArray = new byte[num2 - nWidth];
                for (int index = 0; index < nHeight; ++index)
                {
                    binaryWriter.Write(ResBuf, index * nWidth, nWidth);
                    if (num2 - nWidth > 0)
                        binaryWriter.Write(ResBuf, 0, num2 - nWidth);
                }
                output.Close();
                binaryWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public struct BITMAPFILEHEADER
        {
            public ushort bfType;
            public int bfSize;
            public ushort bfReserved1;
            public ushort bfReserved2;
            public int bfOffBits;
        }

        public struct MASK
        {
            public byte redmask;
            public byte greenmask;
            public byte bluemask;
            public byte rgbReserved;
        }

        public struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }
    }
}
