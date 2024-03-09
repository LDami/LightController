using LightController.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LightController.ViewModels
{
    public class SequencesVM : INotifyPropertyChanged
    {
        private ObservableCollection<Sequence> sequences;
        private Sequence? selectedSequence;

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand SelectSequence { get; private set; }

        public SequencesVM()
        {
            sequences = [];
            selectedSequence = null;
            SelectSequence = new Command(
                execute: (sequence) =>
                {
                    if (sequence is Sequence)
                    {
                        foreach (Sequence seq in sequences)
                        {
                            seq.IsSelected = false;
                        }
                        SelectedSequence = (Sequence)sequence;
                        SelectedSequence.IsSelected = true;
                        Debug.WriteLine($"Device {SelectedSequence.Id} ({SelectedSequence.Name}) selected");
                    }
                    else
                    {
                        throw new Exception("MainPageVM.SelectSequence: sequence is not from a Sequence model");
                    }
                },
                canExecute: (sequence) =>
                {
                    return true;
                }
            );
        }

        public ObservableCollection<Sequence> Sequences
        {
            get
            {
                return sequences;
            }
            private set
            {
                if (sequences != value)
                {
                    sequences = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sequences)));
                }
            }
        }

        public Sequence? SelectedSequence
        {
            get
            {
                return selectedSequence;
            }
            private set
            {
                if (selectedSequence != value)
                {
                    selectedSequence = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedSequence)));
                }
            }
        }

        public void AddSequence(Sequence sequence)
        {
            sequence.PropertyChanged += (sender, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sequence)));
            };
            sequences.Add(sequence);
        }

        public void AddCue(Sequence sequence)
        {
            sequence.AddCue(new Cue());
        }
    }
    public class SequencesVMService
    {
        private static readonly SequencesVM _mySequencesVM = new();

        public static SequencesVM MySequencesVM
        {
            get
            {
                return _mySequencesVM;
            }

        }
    }
}
