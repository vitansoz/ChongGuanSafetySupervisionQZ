using ChongGuanSafetySupervisionQZ.Model;
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
        public static bool IsAdmin { get; set; }
        public static QZ_Deparment CurrentDeparment { get; set; }
    }
}
