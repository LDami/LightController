using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Models
{
    public class Cue()
    {
        //private string _name;
        private ObservableCollection<Parameter> _parameters = [];

        public ObservableCollection<Parameter> Parameters
        {
            get { return _parameters; }
            set
            {
                if(_parameters != value)
                {
                    _parameters = value;

                }
            }
        }

        public void AddColorParameter(DMXDevice device, Color value)
        {
            _parameters.Add(new ColorParameter(device, value));
        }

        public void AddPositionParameter(DMXDevice device, (int, int) value)
        {
            throw new NotImplementedException();
        }
    }
}
