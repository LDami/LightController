using LightController.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace LightController;

public partial class DMXDevicesPage : ContentPage
{
    public DMXDevicePageVM DMXDeviceVM { get; set; }
    public DMXDevicesPage()
	{
		InitializeComponent();
        if(BindingContext is DMXDevicePageVM)
            DMXDeviceVM = (DMXDevicePageVM)this.BindingContext;
        else
        {
            throw new Exception("Could not start DMXDevicesPage: the binding context is null, please set a ContentPage.BindingContext");
        }
        DMXDeviceVM.PropertyChanged += DMXDevicePageVM_PropertyChanged;
        DMXDeviceVM.AddDevice(new DMXDevice("Spot G1", 0));
        DMXDeviceVM.AddDevice(new DMXDevice("Spot G2", 8));
        DMXDeviceVM.AddDevice(new DMXDevice("Spot G3", 16));
        DMXDeviceVM.AddDevice(new DMXDevice("Spot G4", 32));
    }

    private void DMXDevicePageVM_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Debug.WriteLine("DMXDeviceVM property changed: " + e.PropertyName);
    }

    private void SliderColorRed_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DMXDeviceVM.SetColor((int)SliderColorRed.Value, (int)SliderColorGreen.Value, (int)SliderColorBlue.Value);
    }

    private void SliderColorGreen_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DMXDeviceVM.SetColor((int)SliderColorRed.Value, (int)SliderColorGreen.Value, (int)SliderColorBlue.Value);
    }

    private void SliderColorBlue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        DMXDeviceVM.SetColor((int)SliderColorRed.Value, (int)SliderColorGreen.Value, (int)SliderColorBlue.Value);
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
                return devices.Where(x => x.IsSelected).Count() < devices.Count;
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
                return devices.Where(x => x.IsSelected).Count() > 0;
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
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Devices)));
    }

    public void SetColor(int colorR, int colorG, int colorB)
    {
        foreach (var device in devices)
        {
            if (device.IsSelected)
            {
                device.ColorR = colorR;
                device.ColorG = colorG;
                device.ColorB = colorB;
            }
        }
    }
}