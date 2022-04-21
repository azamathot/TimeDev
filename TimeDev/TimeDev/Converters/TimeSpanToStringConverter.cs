using System;
using System.Globalization;
using Xamarin.Forms;

namespace TimeDev.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (TimeSpan)value;
            return (int)t.TotalHours + t.ToString(@"\:mm\:ss");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse(value.ToString());
        }
    }
}
