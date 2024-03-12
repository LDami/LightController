using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LightController.Models
{
    public class Sequence(int id, string name) : INotifyPropertyChanged
    {
        private int _id = id;
        private string _name = name;
        private bool _selected;
        private ObservableCollection<Cue> _cues = [];

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AddNewCue {
            get {
                return new Command(
                    execute: () =>
                    {
                        AddCue(new Cue("New cue"));
                    },
                    canExecute: () =>
                    {
                        return true;
                    }
                );
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            private set
            {
                if (_id != value)
                {
                    _id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
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

        public ObservableCollection<Cue> Cues
        {
            get
            {
                return _cues;
            }
            set
            {
                if (_cues != value)
                {
                    _cues = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cues)));
                }
            }
        }

        public void AddCue(Cue cue)
        {
            _cues.Add(cue);
            Debug.WriteLine($"Cue added to sequence {_name}");
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
    }
}
