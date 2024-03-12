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
        public event PropertyChangedEventHandler? PropertyChanged;

        public SequencesVM()
        {
            sequences = [];
            selectedSequence = null;
            SelectSequence = new Command(
                execute: (sequence) =>
                {
                    if (sequence is Sequence s)
                    {
                        foreach (Sequence seq in sequences)
                        {
                            seq.IsSelected = false;
                        }
                        SelectedSequence = s;
                        SelectedSequence.IsSelected = true;
                        Debug.WriteLine($"Sequence {SelectedSequence.Id} ({SelectedSequence.Name}) selected");
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
            SelectCue = new Command(
                execute: (cue) =>
                {
                    if (cue is Cue c && SelectedSequence != null)
                    {
                        foreach (Cue c1 in SelectedSequence.Cues)
                        {
                            c1.IsSelected = false;
                        }
                        c.IsSelected = true;
                        SelectedCue = c;
                        TempCue = new("");
                        TempCue.Parameters = new(c.Parameters);
                        tempCueChanged = false;
                        Debug.WriteLine($"Cue '{SelectedCue.Name}' selected and TempCue reinitialized with current cue parameters");
                    }
                    else
                    {
                        throw new Exception("MainPageVM.SelectSequence: cue is not from a Cue model");
                    }
                },
                canExecute: (cue) =>
                {
                    return cue != TempCue;
                }
            );
            OverwriteCue = new Command(
                execute: () =>
                {
                    if (SelectedCue != null && TempCue != null)
                    {
                        tempCueChanged = true;
                        SelectedCue.Parameters = new ObservableCollection<Parameter>(TempCue.Parameters);
                        tempCueChanged = false;
                        Debug.WriteLine($"Cue {SelectedCue.Name} overwrote");
                    }
                },
                canExecute: () =>
                {
                    //return SelectedCue != null && TempCue?.Parameters.Count > 0 && tempCueChanged;
                    return true;
                }
            );
            ClearCue = new Command(
                execute: () =>
                {
                    if (SelectedCue != null)
                    {
                        SelectedCue.Parameters.Clear();
                        Debug.WriteLine($"Cue {SelectedCue.Name} cleared");
                    }
                },
                canExecute: () =>
                {
                    return SelectedCue != null;
                }
            );
        }

        #region Sequences
        private ObservableCollection<Sequence> sequences;
        private Sequence? selectedSequence;
        public ICommand SelectSequence { get; private set; }

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
        #endregion

        #region Cue
        public ICommand SelectCue { get; private set; }
        public ICommand OverwriteCue { get; private set; }
        public ICommand ClearCue { get; private set; }

        private Cue? selectedCue;
        private Cue? tempCue = new("");
        private bool tempCueChanged = false;

        public Cue? SelectedCue
        {
            get
            {
                return selectedCue;
            }
            private set
            {
                if (selectedCue != value)
                {
                    selectedCue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCue)));
                }
            }
        }
        public Cue? TempCue
        {
            get
            {
                return tempCue;
            }
            private set
            {
                if(tempCue != null)
                    tempCue.PropertyChanged -= TempCue_PropertyChanged;
                if (tempCue != value)
                {
                    tempCue = value;
                    if (tempCue != null)
                        tempCue.PropertyChanged += TempCue_PropertyChanged;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TempCue)));
                }
            }
        }

        private void TempCue_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            tempCueChanged = true;
            Debug.WriteLine("TempCue Property Changed: " + e.PropertyName);
        }

        public void AddCue(Sequence sequence)
        {
            Cue newCue = new("");
            sequence.AddCue(newCue);
        }
        #endregion
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
