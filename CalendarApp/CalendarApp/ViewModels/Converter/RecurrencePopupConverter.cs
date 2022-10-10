using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Converter
{
    public class BgColorDayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Color.FromHex("#1a73e8") : Color.FromHex("#f1f3f4");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Color.FromHex("#1a73e8") : Color.FromHex("#f1f3f4");
        }
    }

    public class TextColorDayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Color.White : Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Color.White : Color.Black;
        }
    }
}
