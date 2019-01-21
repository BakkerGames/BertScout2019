using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            App app = Application.Current as App;
            CurrentFRCEventLabel.Text = app.CurrentFRCEvent;
        }

        async private void MainPage_Options_Clicked(object sender, EventArgs e)
        {
            //todo add options page
        }

        async private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectEventPage());
        }

        async private void Button_Match_Scouting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectEventTeamPage());
        }
    }
}