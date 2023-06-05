using Stripe.Checkout;
using Stripe;
using UIMock.API;
using UIMock.Entities;

namespace UIMock.ViewModels;

public class CreateSubViewModel : BaseViewModel
{
    public Session StripeSession { get; private set; }
    public CreateSubViewModel()
	{

	}
    public async Task<string> CreateStripeSession()
    {
        StripeConfiguration.ApiKey = "sk_test_51Mhu7wBa7FzCpcu3o5lLh1IocYW9AdSYZaa65RCuPeog0DGMKzxBGDv7TmIbddyt5TJ75k0crIFrthvnqB5kCOTV00oRWufgTs";
        var options = new SessionCreateOptions
        {
            CustomerEmail = User.CurrentUser.Email,
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = User.CurrentUser.Cantine.Price_api, // Replace with your price ID
                    },
                },
            Mode = "subscription",
            SuccessUrl = "https://example.com/success",
            CancelUrl = "https://example.com/cancel",
            
        };

        var service = new SessionService();
        StripeSession = service.Create(options);

        return StripeSession.Id;
    }

    public async Task CancelStripeSubscription()
    {
        StripeConfiguration.ApiKey = "sk_test_51Mhu7wBa7FzCpcu3o5lLh1IocYW9AdSYZaa65RCuPeog0DGMKzxBGDv7TmIbddyt5TJ75k0crIFrthvnqB5kCOTV00oRWufgTs";
        var subscriptionService = new SubscriptionService();

        // Replace 'sub_...' with the ID of the subscription you want to cancel
        var subscription = subscriptionService.Cancel(User.CurrentUser.SubID);



    }

    public async Task<Subscription> GetSubscription(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);
        var subscriptionService = new SubscriptionService();
        var subscription = subscriptionService.Get(session.SubscriptionId);
        return subscription;
    }

    public async Task<Result.ResultCode> UpdateSubscription(string userid, string subid, string subitemid)
    {
        APIController api = new APIController();
        Result.ResultCode result = await api.UpdateSubscription(userid, subid, subitemid);
        if(result == Result.ResultCode.Success)
        {
            User.CurrentUser.SubID = subid;
            User.CurrentUser.SubItemID = subitemid;
        }
        return result;
    }
}