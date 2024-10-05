using LightController.Models;
using LightController.ViewModels;

namespace LightController;

public partial class DMXDeviceTypeCreatePage : ContentPage
{
	public DMXDeviceTypeCreatePage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        DMXDeviceType deviceType = (DMXDeviceType)this.BindingContext;
        if(deviceType.Name == "")
        {
            this.InputName.SetDynamicResource(StyleProperty, "MandatoryButNotSet");
            return;
        }
        if (deviceType.ColorEnabled)
        {
            /*
            Dictionary<ChannelParameter, int> _parametersMapping = new()
            {
                { ChannelParameter.ColorR, (int)InputColorR.Value },
                { ChannelParameter.ColorG, (int)InputColorG.Value },
                { ChannelParameter.ColorB, (int)InputColorB.Value }
            };
            deviceType.ParametersMapping = _parametersMapping;
            */
        }
        DMXDevicesVMService.MyDMXDevicesVM.AddDeviceType.Execute(deviceType);
        await Navigation.PopAsync();
    }

    private void BtnAddChannel_Clicked(object sender, EventArgs e)
    {
        DMXDeviceType deviceType = (DMXDeviceType)this.BindingContext;
        deviceType.ParametersMapping.Add(new ParameterData(ChannelParameter.None, 0));
    }

    private void SwitchColorEnabled_Toggled(object sender, ToggledEventArgs e)
    {

    }

    private void SwitchPositionEnabled_Toggled(object sender, ToggledEventArgs e)
    {

    }

    private void InputName_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(e.NewTextValue != "")
            this.InputName.RemoveDynamicResource(StyleProperty);
        else
            this.InputName?.SetDynamicResource(StyleProperty, "MandatoryButNotSet");
    }
}