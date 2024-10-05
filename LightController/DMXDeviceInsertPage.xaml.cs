using LightController.Models;
using LightController.ViewModels;
using System.ComponentModel;

namespace LightController;

public partial class DMXDeviceInsertPage : ContentPage
{
	public DMXDeviceInsertPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DMXDeviceTypeCreatePage());
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        
    }
}
