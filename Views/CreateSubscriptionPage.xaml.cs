using UIMock.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics;
using UIMock.API;
using UIMock.Entities;

namespace UIMock.Views;

public partial class CreateSubscriptionPage : ContentPage
{
    private readonly CreateSubViewModel viewModel;
    public CreateSubscriptionPage()
    {
        InitializeComponent();
        viewModel = new CreateSubViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var sessionId = await viewModel.CreateStripeSession();

        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        stripeWebView.Source = session.Url;
    }

    private async void StripeWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        if (e.Url == "https://example.com/success" || e.Url == "https://example.com/cancel")
        {
            stripeWebView.IsVisible = false;
            stripeWebView.IsEnabled = false;
            // Payment is complete, remove the WebView from its parent container
            try
            {
                var viewModel = (CreateSubViewModel)BindingContext;
                var subscription = await viewModel.GetSubscription(viewModel.StripeSession.Id);

                if (subscription != null)
                {
                    Result.ResultCode result = await viewModel.UpdateSubscription(User.CurrentUser.UserID, subscription.Id, subscription.Items.Data[0].Id);
                    if(result == Result.ResultCode.Success)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
                    } else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
                    }
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
                    Console.WriteLine("Subscription not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing payment response: {ex.Message}");
            }
            await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }
    }
}