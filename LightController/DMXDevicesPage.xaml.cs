using LightController.Models;
using LightController.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace LightController;

public partial class DMXDevicesPage : ContentPage
{
    public DMXDevicePageVM DMXDeviceVM { get; set; } // Obsolete ?
    public DMXDevicesPage()
	{
		InitializeComponent();
        DMXDevicesVMService.MyDMXDevicesVM.PropertyChanged += DMXDevicePageVM_PropertyChanged;
        DMXDevicesVMService.MyDMXDevicesVM.Devices.Add(new DMXDevice("Spot G1", 0));
        DMXDevicesVMService.MyDMXDevicesVM.Devices.Add(new DMXDevice("Spot G2", 8));
        DMXDevicesVMService.MyDMXDevicesVM.Devices.Add(new DMXDevice("Spot G3", 16));
        DMXDevicesVMService.MyDMXDevicesVM.Devices.Add(new DMXDevice("Spot G4", 32));
    }

    private void DMXDevicePageVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Debug.WriteLine("DMXDeviceVM property changed: " + e.PropertyName);
        UpdateParameterLabels();
    }

    private void SliderColorRed_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateDevicesColor();
        UpdateParameterLabels();
    }

    private void SliderColorGreen_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateDevicesColor();
        UpdateParameterLabels();
    }

    private void SliderColorBlue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateDevicesColor();
        UpdateParameterLabels();
    }
    private void UpdateDevicesColor()
    {
        foreach (DMXDevice device in DMXDevicesVMService.MyDMXDevicesVM.Devices)
        {
            if (device.IsSelected)
            {
                device.ColorR = (int)SliderColorRed.Value;
                device.ColorG = (int)SliderColorGreen.Value;
                device.ColorB = (int)SliderColorBlue.Value;
            }
        }
    }
    private void UpdateParameterLabels()
    {
        Color selectedColor = Colors.Beige;
        if (SequencesVMService.MySequencesVM.TempCue?.Parameters.Where(p => p is ColorParameter).Any() == true)
        {
            LabelColorParameter.BackgroundColor = selectedColor;
        }
        else
            LabelColorParameter.BackgroundColor = Colors.Black;
    }

    private async void BtnCreateDevice_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DMXDeviceInsertPage());
    }
}

public class DMXDevicePageVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand AddDeviceCommand { get; private set; }
    public ICommand SelectAllCommand { get; private set; }
    public ICommand DeselectAllCommand { get; private set; }

    ObservableCollection<DMXDevice> devices;

    public DMXDevicePageVM()
    {
        devices = [];
        AddDeviceCommand = new Command(
            execute: () =>
            {
                AddDevice(new DMXDevice("new", 0));
            },
            canExecute: () =>
            {
                return true;
            }
        );
        SelectAllCommand = new Command(
            execute: () =>
            {
                foreach (DMXDevice device in devices)
                {
                    device.IsSelected = true;
                }
            },
            canExecute: () =>
            {
                return true;
            }
        );
        DeselectAllCommand = new Command(
            execute: () =>
            {
                foreach (DMXDevice device in devices)
                {
                    device.IsSelected = false;
                }
            },
            canExecute: () =>
            {
                return true;
            }
        );
    }

    public ObservableCollection<DMXDevice> Devices
    {
        get
        {
            return devices;
        }
        private set
        {
            if (devices != value)
            {
                devices = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Devices)));
            }
        }
    }

    public void AddDevice(DMXDevice device)
    {
        device.PropertyChanged += (sender, e) =>
        {
            Debug.WriteLine("device property changed: " + e.PropertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DMXDevice)));
        };
        devices.Add(device);
    }

    public void SetColor(int colorR, int colorG, int colorB)
    {
        if (SequencesVMService.MySequencesVM.TempCue is null)
            throw new Exception("SequencesVMService.MySequencesVM.TempCue is null");
        foreach (var device in devices)
        {
            if (device.IsSelected)
            {
                device.ColorR = colorR;
                device.ColorG = colorG;
                device.ColorB = colorB;
                SequencesVMService.MySequencesVM.TempCue.AddColorParameter(device, new Color(colorR, colorG, colorB));
            }
        }
    }
}