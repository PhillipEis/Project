using UIMock.ViewModels;
using UIMock.Entities;
using System.Timers;

namespace UIMock.Views;

public partial class QRCodePage : ContentPage
{
    private System.Timers.Timer timestampTimer;
    private System.Timers.Timer qrCodeTimer;
    private QRCodeHelper qRCodeHelper = new QRCodeHelper();
    public QRCodePage()
	{
		InitializeComponent();
        BindingContext = new QRCodePageViewModel();

        // Initialize the timestamp timer
        timestampTimer = new System.Timers.Timer();
        timestampTimer.Interval = 1000; // Interval in milliseconds
        timestampTimer.Elapsed += TimestampTimer_Tick;
        timestampTimer.Start();

        // Initialize the QR code timer
        qrCodeTimer = new System.Timers.Timer();
        qrCodeTimer.Interval = 1000; // Interval in milliseconds
        qrCodeTimer.Elapsed += QRCodeTimer_Tick;
        qrCodeTimer.Start();
    }

    private void TimestampTimer_Tick(object sender, ElapsedEventArgs e)
    {
        timestampLabel.Dispatcher.Dispatch(() =>
        {
            timestampLabel.Text = DateTime.Now.ToString("H:mm:ss");
        });
    }

    private async void QRCodeTimer_Tick(object sender, ElapsedEventArgs e)
    {
        var qrCodeSource = await qRCodeHelper.LoadQRCode();
        qrCodeImage.Dispatcher.Dispatch(() =>
        {
            qrCodeImage.Source = qrCodeSource;
        });
    }

    //public async void LoadQR()
    //{
    //    QRCodeHelper qRCodeHelper = new QRCodeHelper();
    //    while (true)
    //    {
    //        qrCodeImage.Source = await qRCodeHelper.LoadQRCode();
    //        await Task.Delay(1000);
    //    }
    //}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Save the selected tab index in the preferences
        Preferences.Set("SelectedTabIndex", TabHost.SelectedIndex);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Retrieve the saved selected tab index from the preferences
        if (Preferences.ContainsKey("SelectedTabIndex"))
        {
            int selectedIndex = Preferences.Get("SelectedTabIndex", 0);

            // Set the selected tab index of the TabHostView
            TabHost.SelectedIndex = selectedIndex;
        }
    }
}