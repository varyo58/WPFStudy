using System;
using System.Windows.Data;
using System.Globalization;

namespace hogehoge
{
    public class DegreeToRadianConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double x = (double)value;
            return x / 180 * Math.PI;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double x = double.Parse((string)value);
            return (x / Math.PI * 180).ToString();
        }
    }
}