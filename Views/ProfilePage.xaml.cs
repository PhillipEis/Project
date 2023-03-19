using UIMock.ViewModels;

namespace UIMock.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		BindingContext = new ProfilePageViewModel();
       
        if (string.IsNullOrEmpty(Entities.User.CurrentUser.SubID))
        {
            btnCreateSubscription.IsVisible = true;
        }
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Retrieve the saved selected tab index from the preferences
        if (Preferences.ContainsKey("SelectedTabIndex"))
        {
            int selectedIndex = Preferences.Get("SelectedTabIndex", 0);

            // Set the selected tab index of the TabHostView
            TabHost.SelectedIndex = selectedIndex;
        }
    }
}