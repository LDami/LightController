using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Models
{
    public class DMXDeviceType : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private bool _colorEnabled;
        private bool _positionEnabled;
        private ObservableCollection<ParameterData> _parametersMapping;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DMXDeviceType()
        {
            _name = string.Empty;
            _description = string.Empty;
            _colorEnabled = false;
            _positionEnabled = false;
            ParameterData defaultParam = new(ChannelParameter.None, 0); 
            _parametersMapping = new ObservableCollection<ParameterData>
            {
                defaultParam
            };
        }

        public int Id
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
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }
        public bool ColorEnabled
        {
            get
            {
                return _colorEnabled;
            }
            set
            {
                if (_colorEnabled != value)
                {
                    _colorEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorEnabled)));
                }
            }
        }
        public bool PositionEnabled
        {
            get
            {
                return _positionEnabled;
            }
            set
            {
                if (_positionEnabled != value)
                {
                    _positionEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PositionEnabled)));
                }
            }
        }
        public ObservableCollection<ParameterData> ParametersMapping
        {
            get
            {
                return _parametersMapping;
            }
            set
            {
                if (_parametersMapping != value)
                {
                    _parametersMapping = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParametersMapping)));
                }
            }
        }
    }

    public enum ChannelParameter
    {
        None = 0,
        ColorR,
        ColorG,
        ColorB,
        PositionPan,
        PositionTilt
    }
    public class ChannelPicker
    {
        public ChannelParameter parameter { get; }
        public string displayString { get; }
        public ChannelPicker(ChannelParameter parameter, string displayString)
        {
            this.parameter = parameter;
            this.displayString = displayString;
        }
    }

    public class ParameterData : INotifyPropertyChanged
    {
        private ChannelParameter _parameter = ChannelParameter.None;
        private int _address;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ParameterData(ChannelParameter channel, int address)
        {
            _parameter = channel;
            _address = address;
        }
        public static IReadOnlyList<ChannelPicker> AllParameters
        {
            get
            {
                List<ChannelPicker> pickers = new List<ChannelPicker>();
                int idx = 0;
                foreach (string name in Enum.GetNames(typeof(ChannelParameter)))
                {
                    pickers.Add(new ChannelPicker((ChannelParameter)idx++, name));
                }
                return pickers;
            }
        }

        public ChannelParameter Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                if (_parameter != value)
                {
                    _parameter = value;
                    PropertyChanged?.Invoke(value, new PropertyChangedEventArgs(nameof(Parameter)));
                }
            }
        }
        public ChannelPicker PickerValue
        {
            get
            {
                return AllParameters.Where(x => x.parameter == _parameter).FirstOrDefault(new ChannelPicker(ChannelParameter.None, "None"));
            }
            set
            {
                _parameter = value.parameter;
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
                    PropertyChanged?.Invoke(value, new PropertyChangedEventArgs(nameof(Address)));
                }
            }
        }
    }
}
