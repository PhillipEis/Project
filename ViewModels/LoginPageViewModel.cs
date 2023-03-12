using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;
using System;
using System.Windows.Input;
using UIMock.API;

namespace UIMock
{
	public class LoginPageViewModel : BaseViewModel
	{
        private readonly APIController api = new APIController();
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public ICommand ICommandNavToHomePage { get; set; }
        public ICommand IOpenRegistration { get; set; }
        private FirebaseAuthClient client;
        public LoginPageViewModel()
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
            ICommandNavToHomePage = new Command(() => LoginBtnTappedAsync());
            IOpenRegistration = new Command(() => RegiBtnTapped());
        }

        private void RegiBtnTapped()
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginBtnTappedAsync()
        {
            try
            {
                var userCredential = await client.SignInWithEmailAndPasswordAsync(Email, UserPassword);
                await api.LoadUser(userCredential.User.Uid);
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
        }
    }
}

