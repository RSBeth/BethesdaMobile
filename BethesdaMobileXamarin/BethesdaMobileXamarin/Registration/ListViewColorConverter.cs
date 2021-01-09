using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.Registration
{
    public class ListViewColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                if (value != null)
                {
                    if ((string)value == "true")
                        return Color.Black;
                    else
                        return Color.Red;
                }
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
