
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Converter
{
    public class DatetimeLocaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            culture = new CultureInfo("vi-VN");
            var dateTime = (DateTime)value;
            return culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            culture = new CultureInfo("vi-VN");
            var dateTime = (DateTime)value;
            return culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);
        }
    }
}
