using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KPT1
{
    public class ArchivedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte)
            {
                return (byte)value == 1 ? "Yes" : "No";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return (string)value == "Yes" ? (byte)1 : (byte)0;
            }
            throw new InvalidOperationException("Invalid value");
        }
    }

}
