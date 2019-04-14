using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChongGuanSafetySupervisionQZ.View.WPF.Common;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Helpers
{
    public class DirectXSoundHelper
    {
        private Capture mCapDev = (Capture)null;
        private CaptureBuffer mRecBuffer = (CaptureBuffer)null;
        private int mNextCaptureOffset = 0;
        private int mSampleCount = 0;
        private Notify mNotify = (Notify)null;
        private int mNotifySize = 0;
        private int mBufferSize = 0;
        private Thread mNotifyThread = (Thread)null;
        private AutoResetEvent mNotificationEvent = (AutoResetEvent)null;
        private string mFileName = string.Empty;
        private FileStream mWaveFile = (FileStream)null;
        private BinaryWriter mWriter = (BinaryWriter)null;
        private WaveFormat mWavFormat;
        public const int cNotifyNum = 16;

        private bool _isPaused;

        public event EventHandler<DirectXSoundEventArgs> SoundDataGet;

        public DirectXSoundHelper()
        {

        }

        public void Init()
        {
            this.InitCaptureDevice();
            this.mWavFormat = this.CreateWaveFormat();
        }

        private bool InitCaptureDevice()
        {
            CaptureDevicesCollection devicesCollection = new CaptureDevicesCollection();
            Guid empty = Guid.Empty;
            if (devicesCollection.Count <= 0)
                return false;
            Guid guidDev = devicesCollection[0].DriverGuid;
            try
            {
                this.mCapDev = new Capture(guidDev);
            }
            catch (DirectXException ex)
            {
                return false;
            }
            return true;
        }

        private WaveFormat CreateWaveFormat()
        {
            WaveFormat waveFormat = new WaveFormat();
            waveFormat.FormatTag = WaveFormatTag.Pcm;
            waveFormat.SamplesPerSecond = 16000;
            waveFormat.BitsPerSample = (short)16;
            waveFormat.Channels = (short)1;
            waveFormat.BlockAlign = (short)((int)waveFormat.Channels * ((int)waveFormat.BitsPerSample / 8));
            waveFormat.AverageBytesPerSecond = (int)waveFormat.BlockAlign * waveFormat.SamplesPerSecond;
            return waveFormat;
        }

        public void SetFileName(string filename)
        {
            this.mFileName = filename;
        }

        public void RecStart()
        {
            this.CreateCaptureBuffer();
            this.InitNotifications();
            this.mRecBuffer.Start(true);
        }

        public void RecResume()
        {
            _isPaused = false;
        }

        public void RecPause()
        {
            _isPaused = true;
        }

        public void RecStop()
        {
            this.mRecBuffer.Stop();
            if (this.mNotificationEvent != null)
                this.mNotificationEvent.Set();
            this.mNotifyThread.Abort();
            this.RecordCapturedData();
        }

        private void CreateCaptureBuffer()
        {
            CaptureBufferDescription desc = new CaptureBufferDescription();
            if ((Notify)null != this.mNotify)
            {
                this.mNotify.Dispose();
                this.mNotify = (Notify)null;
            }
            if ((CaptureBuffer)null != this.mRecBuffer)
            {
                this.mRecBuffer.Dispose();
                this.mRecBuffer = (CaptureBuffer)null;
            }

            this.mNotifySize = 1024 > this.mWavFormat.AverageBytesPerSecond / 8 ? 1024 : this.mWavFormat.AverageBytesPerSecond / 8;
            this.mNotifySize -= this.mNotifySize % (int)this.mWavFormat.BlockAlign;
            this.mBufferSize = this.mNotifySize * 16;
            desc.BufferBytes = this.mBufferSize;
            desc.Format = this.mWavFormat;
            this.mRecBuffer = new CaptureBuffer(desc, this.mCapDev);
            this.mNextCaptureOffset = 0;
        }

        private bool InitNotifications()
        {
            if ((CaptureBuffer)null == this.mRecBuffer)
                return false;

            this.mNotificationEvent = new AutoResetEvent(false);
            if (this.mNotifyThread == null)
            {
                this.mNotifyThread = new Thread(new ThreadStart(this.WaitThread));
                this.mNotifyThread.Start();
            }
            BufferPositionNotify[] notify = new BufferPositionNotify[17];

            for (int index = 0; index < 16; ++index)
            {
                notify[index].Offset = this.mNotifySize * index + this.mNotifySize - 1;
                notify[index].EventNotifyHandle = this.mNotificationEvent.SafeWaitHandle.DangerousGetHandle();
            }
            this.mNotify = new Notify(this.mRecBuffer);
            this.mNotify.SetNotificationPositions(notify, 16);
            return true;
        }

        private void WaitThread()
        {
            while (true)
            {
                this.mNotificationEvent.WaitOne(-1, true);

                if (!_isPaused)
                {
                    this.RecordCapturedData();
                }
            }
        }

        private void RecordCapturedData()
        {
            int currentReadPosition = 0;
            int currentCapturePosition = 0;
            this.mRecBuffer.GetCurrentPosition(out currentCapturePosition, out currentReadPosition);
            int num1 = currentReadPosition - this.mNextCaptureOffset;
            if (num1 < 0)
                num1 += this.mBufferSize;
            int num2 = num1 - num1 % this.mNotifySize;
            if (num2 == 0)
                return;
            byte[] audioData = (byte[])this.mRecBuffer.Read(this.mNextCaptureOffset, typeof(byte), LockFlag.None, new int[1]
            {
                num2
            });

            if (SoundDataGet != null)
            {
                SoundDataGet(this, new DirectXSoundEventArgs { AudioData = audioData });
            }

            this.mSampleCount += audioData.Length;
            this.mNextCaptureOffset += audioData.Length;
            this.mNextCaptureOffset %= this.mBufferSize;
        }


        private void CreateSoundFile()
        {
            this.mWaveFile = new FileStream(this.mFileName, FileMode.Create);
            this.mWriter = new BinaryWriter((Stream)this.mWaveFile);
            char[] chars1 = new char[4] { 'R', 'I', 'F', 'F' };
            char[] chars2 = new char[4] { 'W', 'A', 'V', 'E' };
            char[] chars3 = new char[4] { 'f', 'm', 't', ' ' };
            char[] chars4 = new char[4] { 'd', 'a', 't', 'a' };
            short num1 = 1;
            int num2 = 16;
            int num3 = 0;
            short num4 = 0;
            if ((short)8 == this.mWavFormat.BitsPerSample && (short)1 == this.mWavFormat.Channels)
                num4 = (short)1;
            else if ((short)8 == this.mWavFormat.BitsPerSample && (short)2 == this.mWavFormat.Channels || (short)16 == this.mWavFormat.BitsPerSample && (short)1 == this.mWavFormat.Channels)
                num4 = (short)2;
            else if ((short)16 == this.mWavFormat.BitsPerSample && (short)2 == this.mWavFormat.Channels)
                num4 = (short)4;
            this.mWriter.Write(chars1);
            this.mWriter.Write(num3);
            this.mWriter.Write(chars2);
            this.mWriter.Write(chars3);
            this.mWriter.Write(num2);
            this.mWriter.Write(num1);
            this.mWriter.Write(this.mWavFormat.Channels);
            this.mWriter.Write(this.mWavFormat.SamplesPerSecond);
            this.mWriter.Write(this.mWavFormat.AverageBytesPerSecond);
            this.mWriter.Write(num4);
            this.mWriter.Write(this.mWavFormat.BitsPerSample);
            this.mWriter.Write(chars4);
            this.mWriter.Write(0);
        }

    }
}
