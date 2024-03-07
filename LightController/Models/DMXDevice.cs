using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LightController.Models
{
    public class DMXDevice : INotifyPropertyChanged
    {
        private Guid _id;
        private string _name;
        private int _address;
        private bool _enabled;
        private bool _selected;

        // Parameters
        private int _colorR;
        private int _colorG;
        private int _colorB;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand ToggleSelect { get; private set; }

        public DMXDevice(string name, int address)
        {
            _id = Guid.NewGuid();
            _name = name;
            _address = address;
            _enabled = true;
            Random rdm = new Random();
            _colorR = rdm.Next(0, 255);
            _colorG = rdm.Next(0, 255);
            _colorB = rdm.Next(0, 255);

            ToggleSelect = new Command(
                execute: () =>
                {
                    IsSelected = !_selected;
                    Console.WriteLine($"Device {Name} toggled selected, new value: {IsSelected}");
                },
                canExecute: () =>
                {
                    return true;
                }
            );
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public int Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
                }
            }
        }

        public int ColorR
        {
            get
            {
                return _colorR;
            }
            set
            {
                if (_colorR != value)
                {
                    _colorR = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }

        public int ColorG
        {
            get
            {
                return _colorG;
            }
            set
            {
                if (_colorG != value)
                {
                    _colorG = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }

        public int ColorB
        {
            get
            {
                return _colorB;
            }
            set
            {
                if (_colorB != value)
                {
                    _colorB = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }

        public string Color
        {
            get
            {
                return "#" + _colorR.ToString("X2") + _colorG.ToString("X2") + _colorB.ToString("X2");
            }
            set
            {
                System.Drawing.Color.FromName(value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }

    }
}
