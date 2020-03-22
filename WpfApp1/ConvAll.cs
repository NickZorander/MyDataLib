using DataLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfApp1
{
    class ConvAll : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Programmer)
                return "Programmer" + ((Person)value).name_and_surname[0] + " " + ((Person)value).name_and_surname[1];

            if (value is Researcher)
                return "Researcher" + ((Person)value).name_and_surname[0] + " " + ((Person)value).name_and_surname[1];

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
