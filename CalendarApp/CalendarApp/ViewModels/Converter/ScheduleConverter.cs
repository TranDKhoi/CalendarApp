using CalendarApp.Models;
using CalendarApp.ViewModels.Schedule;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Xamarin.Forms;

namespace CalendarApp.ViewModels.Converter
{
    public class DayColorTabItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return (isSelected) ? Color.FromHex("49B583") : Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool isSelected = (bool)value;
            return (isSelected) ? Color.FromHex("49B583") : Color.Black;
        }
    }

    public class TitleColorTabItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return (isSelected) ? Color.FromHex("49B583") : Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool isSelected = (bool)value;
            return (isSelected) ? Color.FromHex("49B583") : Color.Gray;
        }
    }

    public class IsEventOrCourseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? courseId = (int?)value;
            return (courseId != null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? courseId = (int?)value;
            return (courseId != null);
        }
    }
}
