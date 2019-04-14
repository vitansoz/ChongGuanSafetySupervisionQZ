using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Converters
{
    public class IsRunningStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "videos")
            {
                if ((bool)value)
                {
                    return "监控中...";
                }
                else
                {
                    return "未开始";
                }
            }

            if (parameter != null && parameter.ToString() == "talking")
            {
                if ((bool)value)
                {
                    return "已进行";
                }
                else
                {
                    return "未开始";
                }
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "未开始")
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
