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
    }
}
