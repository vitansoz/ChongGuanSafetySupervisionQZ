using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Converters
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                if ((bool)value)
                {
                if (parameter != null && parameter.ToString().ToLower() == "reversal")
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            else
            {
                if (parameter != null && parameter.ToString().ToLower() == "reversal")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return ((Visibility)value == Visibility.Visible) && (parameter.ToString().ToLower() != "reversal");

            if (parameter != null && parameter.ToString().ToLower() == "reversal")
            {
                return (Visibility)value != Visibility.Visible;
            }
            else
            {
                return (Visibility)value == Visibility.Visible;
            }
        }
    }
}
