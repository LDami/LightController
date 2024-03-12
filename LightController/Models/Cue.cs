using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LightController.Models
{
    public class Cue(string name) : INotifyPropertyChanged
    {
        private string _name = name;
        private bool _selected;
        private ObservableCollection<Parameter> _parameters = [];

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public ObservableCollection<Parameter> Parameters
        {
            get { return _parameters; }
            set
            {
                if(_parameters != value)
                {
                    _parameters = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parameters)));
                }
            }
        }

        public void AddColorParameter(DMXDevice device, Color value)
        {
            bool paramAlreadyExists = false;
            if (_parameters.Count > 0)
            {
                if (_parameters.Where(p => p is ColorParameter && p.Device == device).FirstOrDefault() is ColorParameter parameter)
                {
                    if(parameter != null)
                    {
                        parameter.Current = value;
                        paramAlreadyExists = true;
                        Debug.WriteLine("Parameter already exists and have been overwrote");
                    }
                }
            }
            if(!paramAlreadyExists)
            {
                ColorParameter p = new(device, value);
                p.PropertyChanged += (sender, e) =>
                {
                    Debug.WriteLine("Parameter changed current value: " + p.Current.ToString());
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Parameters)));
                };
                _parameters.Add(p);
                Debug.WriteLine("Parameter doesn't exists and have been created");
            }
        }

        public void AddPositionParameter(DMXDevice device, (int, int) value)
        {
            throw new NotImplementedException();
        }
    }
}
