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
        public string Name { get; private set; }
        public string Greeting { get; private set; }
        public string OpenHours { get; private set; }

        public HomePageViewModel()
        {
            Name = User.CurrentUser.Name;
            Greeting = GetGreeting();
            OpenHours = User.CurrentUser.Cantine.Details.OpenTime + "-" + User.CurrentUser.Cantine.Details.CloseTime;
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
            TimeSpan morningStart = new TimeSpan(6, 0, 0); // 6:00 AM
            TimeSpan morningEnd = new TimeSpan(11, 59, 59); // 11:59:59 AM
            TimeSpan afternoonStart = new TimeSpan(12, 0, 0); // 12:00 PM
            TimeSpan afternoonEnd = new TimeSpan(17, 59, 59); // 5:59:59 PM
            TimeSpan eveningStart = new TimeSpan(18, 0, 0); // 6:00 PM
            TimeSpan eveningEnd = new TimeSpan(23, 59, 59); // 11:59:59 PM
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            string greeting;
            if (currentTime >= morningStart && currentTime <= morningEnd)
            {
                greeting = "Godmorgen, ";
            }
            else if (currentTime >= afternoonStart && currentTime <= afternoonEnd)
            {
                greeting = "God eftermiddag, ";
            }
            else if (currentTime >= eveningStart && currentTime <= eveningEnd)
            {
                greeting = "God aften, ";
            }
            else
            {
                greeting = "God nat, ";
            }

            return greeting;
        }

    }
}

