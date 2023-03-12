using UIMock.API;

namespace UIMock;

public partial class App : Application
{
	public APIController Controller = new APIController();
	public App()
	{

		InitializeComponent();
		LoadCantinesAsync();
        MainPage = new NavigationPage(new LoginPage());
	}

	private async void LoadCantinesAsync()
	{
        await Controller.LoadCantines();
    }
}

