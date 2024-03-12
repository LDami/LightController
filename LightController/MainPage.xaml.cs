using LightController.Models;
using LightController.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LightController
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            SequencesVMService.MySequencesVM.PropertyChanged += MainVM_PropertyChanged;
            SequencesVMService.MySequencesVM.AddSequence(new Sequence(0, "Sequence #1"));
        }

        private void MainVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("MainVM property changed: " + e.PropertyName);
        }

        private void BtnCreateSequence_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnCreateSequence_Clicked called");
            SequencesVMService.MySequencesVM.AddSequence(new Sequence(SequencesVMService.MySequencesVM.Sequences.Count, "Sequence #" + (SequencesVMService.MySequencesVM.Sequences.Count + 1)));
        }

        private void BtnWindowDevices_Clicked(object sender, EventArgs e)
        {
            // interesting way to switch page while staying on same window
            //await Navigation.PushAsync(new DMXDevicesPage());
            Application.Current?.OpenWindow(new Window { Page = new DMXDevicesPage() });
        }
    }
}
