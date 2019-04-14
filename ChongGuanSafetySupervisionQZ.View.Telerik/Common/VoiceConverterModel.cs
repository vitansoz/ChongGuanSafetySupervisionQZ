using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Common
{

    public class VoiceConverterModel
    {
        public int SegmentCount { get; set; }
        public Segment[] Segment { get; set; }
    }

    public class Segment
    {
        public int SegmentIndex { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }

}
