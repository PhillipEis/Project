using Sharpnado.Tabs;

namespace UIMock;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Save the selected tab index in the preferences
        Preferences.Set("SelectedTabIndex", TabHost.SelectedIndex);
    }

}
