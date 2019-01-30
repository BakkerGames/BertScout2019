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
            MainPage_Version_Label.Text = $"Version {App.dbVersion}";
            CurrentFRCEventLabel.Text = App.currFRCEventName;
            LabelVersionMessage.Text = "";
        }

        async private void MainPage_Options_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage());
        }

        async private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e)
        {
            //todo add wait cursor here
            Button_Select_FRC_Event.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new SelectEventPage());
            Button_Select_FRC_Event.BackgroundColor = Color.Default;
        }

        async private void Button_Match_Scouting_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(App.currFRCEventKey))
            {
                return;
            }
            //todo add wait cursor here
            Button_Match_Scouting.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new SelectEventTeamPage());
            Button_Match_Scouting.BackgroundColor = Color.Default;
        }

        private void MainPage_Version_Label_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LabelVersionMessage.Text))
            {
                LabelVersionMessage.Text = "Please don't press that button!";
            }
            else
            {
                LabelVersionMessage.Text = "";
            }
        }
    }
}