using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using System;
using System.Windows.Input;
using UIMock.Entities;
using UIMock.API;
using System.Net.Http;
using System.Net.Http.Json;
using System.Diagnostics;

namespace UIMock
{
	public class RegisterPageViewModel : BaseViewModel
	{
        #region Variables and Properties
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public ICommand IRegister { get; set; }
        private FirebaseAuthClient client;
        private readonly APIController api = new APIController();
        private List<Cantines> _cantines = new List<Cantines>();
        public List<Cantines> Cantines
        {
            get { return _cantines; }
            set { _cantines = value; OnPropertyChanged(); }
        }
        public Cantines SelectedCantine { get; set; }
        #endregion
        public RegisterPageViewModel()
		{
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCML0uwVQuamjYGtp70j53DXyIu6sI2uM8",
                AuthDomain = "projectkasper.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
    {
                    new EmailProvider()
    },
                UserRepository = new FileUserRepository("FirebaseSample") // persist data into %AppData%\FirebaseSample
            };
            client = new FirebaseAuthClient(config);
            LoadCantinesAsync();
            IRegister = new Command(() => RegisterBtn());
        }

        private async void RegisterBtn()
        {
            try
            {
                var auth = await client.CreateUserWithEmailAndPasswordAsync(Email, UserPassword);
                await api.CreateUser(auth.User.Uid, Name, Email, Phone, SelectedCantine.Id);
                await App.Current.MainPage.DisplayAlert("Alert", "Bruger oprettet", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task LoadCantinesAsync()
        {
            Cantines = await api.GetCantines();
        }
    }
}

