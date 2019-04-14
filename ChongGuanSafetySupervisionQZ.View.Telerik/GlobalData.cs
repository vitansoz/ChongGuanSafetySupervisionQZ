using ChongGuanSafetySupervisionQZ.Model;
using ChongGuanSafetySupervisionQZ.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    public class GlobalData
    {
        public static QZ_User CurrentUser { get; set; }
        public static QZ_Role CurrnetRole { get; set; }
        public static QZ_Deparment CurrentDeparment { get; set; }

        public static CheckingHardwareViewModel CheckingHardwareViewModel { get; set; }
        public static NewTalkViewModel NewTalkViewModel { get; set; }
        public static TalkingPageViewModel TalkingPageViewModel { get; set; }
        public static TalkingRecordManageViewModel TalkingRecordManageViewModel { get; set; }
        public static SatisticsPageViewModel SatisticsPageViewModel { get; set; }

        public static DateTime CurrentTalkingStartTime { get; set; }
        public static DateTime CurrentTalkingEndTime { get; set; }

    }
}
