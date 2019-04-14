using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Converters
{
    public class MessageTypeIsPartyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "回答中";
            }
            else
            {
                return "提问中";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "提问中")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
