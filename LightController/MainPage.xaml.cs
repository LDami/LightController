using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace LightController
{
    // Sequence -> Cue
    public partial class MainPage : ContentPage
    {
        public MainPageVM MainVM { get; set; }
        public MainPage()
        {
            InitializeComponent();
            MainVM = new();
            MainVM.PropertyChanged += MainVM_PropertyChanged;
            MainVM.AddSequence(new SequenceViewModel());
            this.BindingContext = MainVM;
        }

        private void MainVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("MainVM property changed: " + e.PropertyName);
        }

        private void BtnCreateSequence_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("BtnCreateSequence_Clicked called");
            MainVM.AddSequence(new SequenceViewModel());
            //SemanticScreenReader.Announce(CounterBtn.Text); // may be for visually deficient people
        }
    }

    public class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        List<SequenceViewModel> sequences;

        public MainPageVM()
        {
            sequences = [];
        }

        public List<SequenceViewModel> Sequences
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

        public void AddSequence(SequenceViewModel sequence)
        {
            sequence.PropertyChanged += (sender, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sequences)));
            };
            sequences.Add(sequence);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sequence)));
        }
    }

    public class SequenceViewModel : INotifyPropertyChanged
    {
        static int counter = 0;

        string name;
        List<Cue> cueList;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public SequenceViewModel()
        {
            name = $"Sequence #{counter++}";
            cueList = [];
        }

        public void AddCue(Cue cue)
        {
            cueList.Add(cue);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
    }

    public class Sequence()
    {

    }

    public class Cue
    {
        public Cue()
        {

        }
    }
}
