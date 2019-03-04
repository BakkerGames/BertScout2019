using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool _syncFlag = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MainPage_Version_Label.Text = $"Version {App.dbVersion}";
            CurrentFRCEventLabel.Text = App.currFRCEventName;
            LabelVersionMessage.Text = "";
            if (string.IsNullOrEmpty(App.currFRCEventName))
            {
                LabelVersionMessage.Text = "Please select an event";
            }
            Button_Match_Scouting.IsEnabled = (!string.IsNullOrEmpty(App.currFRCEventName));
        }

        async private void MainPage_Options_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            _syncFlag = true;
            await Navigation.PushAsync(new OptionsPage());
            _syncFlag = false;
        }

        async private void Button_Select_FRC_Event_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            _syncFlag = true;
            Button_Select_FRC_Event.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new SelectEventPage());
            Button_Select_FRC_Event.BackgroundColor = Color.Default;
            _syncFlag = false;
        }

        async private void Button_Match_Scouting_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.currFRCEventKey))
            {
                return;
            }
            _syncFlag = true;
            Button_Match_Scouting.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new SelectEventTeamPage());
            Button_Match_Scouting.BackgroundColor = Color.Default;
            _syncFlag = false;
        }

        async private void Button_Event_Results_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            if (string.IsNullOrEmpty(App.currFRCEventKey))
            {
                return;
            }
            _syncFlag = true;
            Button_Event_Results.BackgroundColor = App.SelectedButtonColor;
            await Navigation.PushAsync(new ResultPage());
            Button_Event_Results.BackgroundColor = Color.Default;
            _syncFlag = false;
        }

        private void MainPage_Version_Label_Clicked(object sender, EventArgs e)
        {
            if (_syncFlag)
            {
                return;
            }
            _syncFlag = true;
            if (string.IsNullOrEmpty(LabelVersionMessage.Text))
            {
                LabelVersionMessage.Text = $"This app was coded by:\nScott, Chloe, & Nate\n{App.AppVersionDate}";
            }
            else
            {
                LabelVersionMessage.Text = "";
            }
            _syncFlag = false;
        }
    }
}