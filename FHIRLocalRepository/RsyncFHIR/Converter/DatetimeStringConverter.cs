using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RsyncFHIR.Converter
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime)value;
            return dt.ToString("HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            DateTime rtn;
            if (DateTime.TryParse(s, out rtn))
            {
                return rtn;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bl = (bool)value;
            return bl ? "○" : "×";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            return s == "○";
        }
    }
}
