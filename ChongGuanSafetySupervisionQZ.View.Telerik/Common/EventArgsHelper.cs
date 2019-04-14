using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Common
{
    public class DirectXSoundEventArgs : EventArgs
    {
        public byte[] AudioData { get; set; }
    }

    public class AudioDataAvailableEventArgs : EventArgs
    {
        public float MasterPeakValue { get; set; }
    }
}
