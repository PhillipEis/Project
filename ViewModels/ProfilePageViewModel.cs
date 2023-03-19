using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIMock.Views;

namespace UIMock.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICommand ICreateSubscription { get; set; }
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
                        case 1:
                            // Navigate to the List page
                            break;
                        case 2:
                            // Handle the Circle button tab
                            break;
                        case 3:
                            // Navigate to the Grid page
                            break;
                        case 4:
                            // Navigate to the second Home page

                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public ProfilePageViewModel()
        {
            Name = Entities.User.CurrentUser.Name;
            Email = Entities.User.CurrentUser.Email;
            Phone = $"+45 {Entities.User.CurrentUser.Phone}";
            ICreateSubscription = new Command(() => CreateSubscriptBtnTapped());
        }

        private async void CreateSubscriptBtnTapped()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateSubscriptionPage());
        }

    }
}
