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

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Save the selected tab index in the preferences
        Preferences.Set("SelectedTabIndex", TabHost.SelectedIndex);
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