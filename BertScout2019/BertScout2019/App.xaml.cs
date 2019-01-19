using BertScout2019.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BertScout2019
{
    public partial class App : Application
    {
        const string frcEventProperty = "currentFRCEvent";
        public string CurrentFRCEvent { get; set; }

        public App()
        {
            if (Properties.ContainsKey(frcEventProperty))
            {
                CurrentFRCEvent = (string)Properties[frcEventProperty];
            }
            InitializeComponent();
            MainPage = new MainPage() { BarBackgroundColor = Color.FromHex("#22be1c") };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Properties[frcEventProperty] = CurrentFRCEvent;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
