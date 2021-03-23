using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyVocabularyCards.Behaviors
{
    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = (string)value;

            if (status != null)
            {
                if (status == "Correct!")
                {
                    return Color.Green;
                }
                else if(status =="")
                {
                    return Color.Transparent;
                }
                else
                {
                    return Color.Red;
                }
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
