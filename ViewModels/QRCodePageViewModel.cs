using UIMock.Views;

namespace UIMock.ViewModels;

public class QRCodePageViewModel : BaseViewModel
{
    private int _selectedPage;
    public int SelectedPage
    {
        get => _selectedPage;
        set
        {
            if (SetProperty(ref _selectedPage, value))
            {
                switch (value)
                {
                    case 0:
                        App.Current.MainPage.Navigation.PushAsync(new HomePage());
                        break;
                    case 2:
                        // Handle the Circle button tab
                        break;
                    case 3:
                        // Navigate to the Grid page
                        App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public QRCodePageViewModel()
	{

	}
}