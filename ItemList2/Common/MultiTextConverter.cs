using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ItemList2.Common
{
    /// <summary>
    /// 二つのテキストを半角スペースで結合するコンバーターです。
    /// </summary>
    public class MultiTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string first = (string)values[0];
            string last = (string)values[1];
            return string.Format("{0} {1}", first, last);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            string[] items = name.Split(' ');
            return new string[] { items[0], items[1] };
        }
    }
}
