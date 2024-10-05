using LightController.Models;
using LightController.ViewModels;
using System;
using System.Globalization;

namespace LightController.Converters
{
    public class IsSelectedToColorConverter : IValueConverter
    {
        enum ConvertType {
            EqualToCreateDeviceType
        }
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isSelected = false;
            if(value?.GetType() == typeof(bool))
                isSelected = (bool)(value ?? false);
            if(value?.GetType() == typeof(int))
            {
                int v = (int)(value ?? 0);
                if(((ConvertType)(parameter ?? 0)) == ConvertType.EqualToCreateDeviceType)
                {
                    isSelected = v == DMXDevicesVMService.MyDMXDevicesVM.CreateDeviceType.Id;
                }
            }
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
