using LightController.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace LightController.ViewModels
{
    public class DMXDevicesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public DMXDevicesVM()
        {
            deviceTypes = [];
            devices = [];
            AddDeviceType = new Command(
                execute: (deviceType) =>
                {
                    if(deviceType is DMXDeviceType deviceType1)
                    {
                        deviceTypes.Add(deviceType1);
                    }
                },
                canExecute: (deviceType) =>
                {
                    return true;
                }
            );
            AddDevice = new Command(
                execute: () =>
                {
                    DMXDevice device = new("Unknown", 0);
                    device.PropertyChanged += (sender, e) =>
                    {
                        Debug.WriteLine("device property changed: " + e.PropertyName);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Devices)));
                    };
                    devices.Add(device);
                },
                canExecute: () =>
                {
                    return true;
                }
            );
            SelectAll = new Command(
                execute: () =>
                {
                },
                canExecute: () =>
                {
                    return true;
                }
            );
            DeselectAll = new Command(
                execute: () =>
                {
                },
                canExecute: () =>
                {
                    return true;
                }
            );
            SelectCreateDeviceType = new Command(
                execute: (object param) =>
                {
                    if(param is DMXDeviceType deviceType)
                    {
                        createDeviceType = deviceType;
                    }
                },
                canExecute: (object deviceType) =>
                {
                    return true;
                }
            );
        }
        ObservableCollection<DMXDeviceType> deviceTypes;
        public ICommand AddDeviceType { get; private set; }
        public ObservableCollection<DMXDeviceType> DeviceTypes
        {
            get
            {
                return deviceTypes;
            }
            private set
            {
                if (deviceTypes != value)
                {
                    deviceTypes = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeviceTypes)));
                }
            }
        }

        ObservableCollection<DMXDevice> devices;
        public ICommand AddDevice { get; private set; }
        public ICommand SelectAll { get; private set; }
        public ICommand DeselectAll { get; private set; }
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


        // Creation only
        public ICommand SelectCreateDeviceType { get; private set; }
        private int createStartingAddress;
        public int CreateStartingAddress
        {
            get
            {
                return createStartingAddress;
            }
            set
            {
                if (createStartingAddress != value)
                {
                    createStartingAddress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CreateStartingAddress)));
                }
            }
        }
        private DMXDeviceType createDeviceType;
        public DMXDeviceType CreateDeviceType
        {
            get
            {
                return createDeviceType;
            }
            set
            {
                if (createDeviceType != value)
                {
                    createDeviceType = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CreateDeviceType)));
                }
            }
        }
    }
    public class DMXDevicesVMService
    {
        private static readonly DMXDevicesVM _myDMXDevicesVM = new();

        public static DMXDevicesVM MyDMXDevicesVM
        {
            get
            {
                return _myDMXDevicesVM;
            }

        }
    }
}
