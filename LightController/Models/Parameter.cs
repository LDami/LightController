using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Models
{
    public abstract class Parameter(DMXDevice device) : INotifyPropertyChanged
    {
        protected DMXDevice _device = device;
        protected object? _current = null;

        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual DMXDevice? Device
        {
            get
            {
                return _device;
            }
        }
        public virtual object? Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
            }
        }
    }

    public class ColorParameter : Parameter
    {
        public ColorParameter(DMXDevice device, Color currentColor) : base(device)
        {
            _current = currentColor;
        }

        public override Color Current
        {
            get { return (Color)(_current ?? throw new Exception("Current color is null")); }
        }
    }
}
