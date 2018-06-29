using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public static class Mci
    {
        public const double MaximumLevel = 128.0;

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr oCallback);

        public static void StartLevelMeter()
        {
            Mci.mciSendString(Mci.DefinitionSet.OpenLevelMeterCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static double GetLevel(int count, out double maxLevel, int delayMs)
        {
            double num = 0.0;
            maxLevel = double.NegativeInfinity;
            for (int index = 0; index < count; ++index)
            {
                StringBuilder strReturn = new StringBuilder();
                Mci.mciSendString(Mci.DefinitionSet.StatusLevelCommand, strReturn, 16, IntPtr.Zero);
                double result;
                if (!double.TryParse(((object)strReturn).ToString(), out result))
                    return 0.0;
                num += result;
                if (result > maxLevel)
                    maxLevel = result;
                Thread.Sleep(delayMs);
            }
            return num / (double)count;
        }

        public static double GetLevel()
        {
            double maxLevel;
            return Mci.GetLevel(1, out maxLevel, 0);
        }

        public static void CloseLevelMeter()
        {
            Mci.mciSendString(Mci.DefinitionSet.CloseLevelMeterCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void Open()
        {
            Mci.mciSendString(Mci.DefinitionSet.OpenRecorderCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void Record()
        {
            Mci.mciSendString(Mci.DefinitionSet.RecordCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void Pause()
        {
            Mci.mciSendString(Mci.DefinitionSet.PauseCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void Stop()
        {
            Mci.mciSendString(Mci.DefinitionSet.StopCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void Close()
        {
            Mci.mciSendString(Mci.DefinitionSet.CloseRecorderCommand, (StringBuilder)null, 0, IntPtr.Zero);
        }

        public static void SaveRecording(string fileName)
        {
            Mci.mciSendString(string.Format(Mci.DefinitionSet.SaveCommandFormat, (object)fileName), (StringBuilder)null, 0, IntPtr.Zero);
        }

        private static class DefinitionSet
        {
            public static readonly string OpenLevelMeterCommand = string.Format("open new type waveaudio alias {0}", (object)"soundLevelMeterDevice");
            public static readonly string OpenRecorderCommand = string.Format("open new type waveaudio alias {0}", (object)"soundRecordDevice");
            public static readonly string StatusLevelCommand = string.Format("status {0} level", (object)"soundLevelMeterDevice");
            public static readonly string RecordCommand = string.Format("record {0}", (object)"soundRecordDevice");
            public static readonly string PauseCommand = string.Format("pause {0}", (object)"soundRecordDevice");
            public static readonly string StopCommand = string.Format("stop {0}", (object)"soundRecordDevice");
            public static readonly string CloseRecorderCommand = string.Format("close {0}", (object)"soundRecordDevice");
            public static readonly string CloseLevelMeterCommand = string.Format("close {0}", (object)"soundLevelMeterDevice");
            public static readonly string SaveCommandFormat = string.Format("save {0} \"{{0}}\"", (object)"soundRecordDevice");
            public const string DllName = "winmm.dll";
            public const string LevelMeterDeviceId = "soundLevelMeterDevice";
            public const string SoundRecordDeviceId = "soundRecordDevice";
            public const string OpenCommandFormat = "open new type waveaudio alias {0}";
            public const string StatusLevelCommandFormat = "status {0} level";
            public const string RecordCommandFormat = "record {0}";
            public const string PauseCommandFormat = "pause {0}";
            public const string StopCommandFormat = "stop {0}";
            public const string CloseCommandFormat = "close {0}";
            public const string SaveCommandFormatFormat = "save {0} \"{{0}}\"";
            public const int ReturnNumDigits = 16;
            public const int MaximumLevel = 128;
        }

        public class MciException : ApplicationException
        {
            public long MciErrorCode { get; private set; }

            public MciException(long mciErrorCode)
              : base(string.Format("MCI error {0}", (object)mciErrorCode))
            {
                this.MciErrorCode = mciErrorCode;
            }
        }
    }
}
