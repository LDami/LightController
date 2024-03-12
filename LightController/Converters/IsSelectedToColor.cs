using System;
using System.Globalization;

namespace LightController.Converters
{
    public class IsSelectedToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isSelected = (bool)(value ?? false);
            if (Application.Current != null)
            {
                try
                {
                    Application.Current.Resources.TryGetValue("BorderColorSelected", out object selectedColor);
                    Application.Current.Resources.TryGetValue("BorderColorNotSelected", out object notSelectedColor);
                    return isSelected ? selectedColor : notSelectedColor;
                }
                catch(KeyNotFoundException)
                {
                    return Colors.Black;
                }
            }
            else
                throw new Exception();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
