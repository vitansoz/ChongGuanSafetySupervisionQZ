using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Converters
{
    public class TalkTypeIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "0";
            }

            switch (value.ToString())
            {
                case "对新收押人员首次谈话":
                    return "0";
                case "在押人员诉讼环节发生变化谈话":
                    return "1";
                case "会见律师后思想不稳定、表现异常的谈话":
                    return "2";
                case "在押人员家庭发生变故谈话":
                    return "3";
                case "被加戴械具、处罚前后谈话":
                    return "4";
                case "调换监室的谈话":
                    return "5";
                case "要求反映监室动态的谈话":
                    return "6";
                case "出所前谈话":
                    return "7";
                default:
                    return "0";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return "对新收押人员首次谈话";
                case 1:
                    return "在押人员诉讼环节发生变化谈话";
                case 2:
                    return "会见律师后思想不稳定";
                case 3:
                    return "在押人员家庭发生变故谈话";
                case 4:
                    return "被加戴械具、处罚前后谈话";
                case 5:
                    return "调换监室的谈话";
                case 6:
                    return "要求反映监室动态的谈话";
                case 7:
                    return "出所前谈话";

                default:
                    return "对新收押人员首次谈话";
            }
        }
    }
}
