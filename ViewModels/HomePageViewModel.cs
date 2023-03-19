using System;
using UIMock.Entities;
using UIMock.API;
using System.Linq;
using Sharpnado.Tabs;
using UIMock.Views;

namespace UIMock
{
    public class HomePageViewModel : BaseViewModel
    {
        private List<Menu> _reminderList = new List<Menu>();
        public List<Menu> ReminderList
        {
            get => _reminderList;

            set
            {
                if (_reminderList == value) return;
                _reminderList = value;
                OnPropertyChanged(nameof(ReminderList));
            }
        }
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
                        case 1:
                            // Navigate to the List page
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
        List<string> menuDays = new List<string>();
        public string Name { get; set; }
        public string Greeting { get; set; }

        public HomePageViewModel()
        {
            Name = User.CurrentUser.Name;
            Greeting = GetGreeting();
            if (User.CurrentUser.Cantine != null)
            {
                var cantine = User.CurrentUser.Cantine;
                if (cantine.Details != null)
                {
                    List<Menu> menuDayList = new List<Menu>
                    {
                        new Menu { Day = "Mandag", Meal = cantine.Details.MenuDay1 },
                        new Menu { Day = "Tirsdag", Meal = cantine.Details.MenuDay2 },
                        new Menu { Day = "Onsdag", Meal = cantine.Details.MenuDay3 },
                        new Menu { Day = "Torsdag", Meal = cantine.Details.MenuDay4 },
                        new Menu { Day = "Fredag", Meal = cantine.Details.MenuDay5 },
                        new Menu { Day = "Lørdag", Meal = cantine.Details.MenuDay6 },
                        new Menu { Day = "Søndag", Meal = cantine.Details.MenuDay7 }
                    };

                    _reminderList = menuDayList.Where(md => !string.IsNullOrEmpty(md.Meal)).ToList();
                }
            }
        }
        private string GetGreeting()
        {
            DateTime now = DateTime.Now;

            if (now.Hour < 12)
            {
                return "Godmorgen,";
            }
            else if (now.Hour < 18)
            {
                return "God eftermiddag,";
            }
            else
            {
                return "Godaften,";
            }
        }
    }
}

