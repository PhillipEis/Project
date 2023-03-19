using UIMock.ViewModels;
using Stripe;
using Stripe.Checkout;

namespace UIMock.Views;

public partial class CreateSubscriptionPage : ContentPage
{
	public CreateSubscriptionPage()
	{
		InitializeComponent();
        BindingContext = new CreateSubViewModel();
        CreateSubscription();
    }

	public async Task CreateSubscription()
	{
        StripeConfiguration.ApiKey = "sk_test_51Mhu7wBa7FzCpcu3o5lLh1IocYW9AdSYZaa65RCuPeog0DGMKzxBGDv7TmIbddyt5TJ75k0crIFrthvnqB5kCOTV00oRWufgTs";
        var options = new SessionCreateOptions
        {
            CustomerEmail = Entities.User.CurrentUser.Email,
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = Entities.User.CurrentUser.Cantine.Price_api, // Replace with your price ID
                },
            },
            Mode = "subscription",
            SuccessUrl = "https://example.com/success",
            CancelUrl = "https://example.com/cancel",
        };

        var service = new SessionService();
        var session = service.Create(options);
        stripeWebView.Source = new Uri(session.Url);
        stripeWebView.Navigated += async (sender, args) =>
        {
            if (args.Url == options.SuccessUrl || args.Url == options.CancelUrl)
            {
                // Payment is complete, remove the WebView from its parent container
                stripeWebView.IsVisible = false;
                stripeWebView.IsEnabled = false;


                var sessionService = new SessionService();
                var checkoutSession = sessionService.Get(session.Id);

                if (checkoutSession.PaymentStatus == "paid")
                {
                    if (checkoutSession.SubscriptionId != null)
                    {
                        Console.WriteLine($"Subscription ID");
                    }
                    else
                    {
                        Console.WriteLine("Subscription ID not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Payment not completed.");
                }
            }
        };
    }
}