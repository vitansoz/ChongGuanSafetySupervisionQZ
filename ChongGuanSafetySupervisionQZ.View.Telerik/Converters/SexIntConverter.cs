using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Converters
{
    public class SexIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "-1";
            }

            if (value.ToString() == "女")
            {
                return 1;
            }
            else if (value.ToString() == "男")
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((int)value) == 0)
            {
                return "男";
            }
            else if (((int)value) == 1)
            {
                return "女";
            }
            else
            {
                return "";
            }

        }
    }
}
