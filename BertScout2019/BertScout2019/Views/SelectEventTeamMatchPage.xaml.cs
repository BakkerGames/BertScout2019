using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamMatchPage : ContentPage
    {
        SelectEventTeamMatchesViewModel viewModel;

        public SelectEventTeamMatchPage()
        {
            InitializeComponent();

            ShowNewMatchNumber.Text = (App.highestMatchNumber + 1).ToString();

            BindingContext = viewModel = new SelectEventTeamMatchesViewModel();
        }


        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //todo
            var item = args.SelectedItem as EventTeamMatch;
            if (item == null)
                return;

            //App.currTeamNumber = item.TeamNumber;
            //await Navigation.PushAsync(new SelectEventTeamMatchPage());
        }

        private void AddMatch_Minus_Clicked(object sender, System.EventArgs e)
        {
            int value = int.Parse(ShowNewMatchNumber.Text);
            if (value > 1)
            {
                ShowNewMatchNumber.Text = (value - 1).ToString();
            }
        }

        private void AddMatch_Plus_Clicked(object sender, System.EventArgs e)
        {
            int value = int.Parse(ShowNewMatchNumber.Text);
            if (value < 999)
            {
                ShowNewMatchNumber.Text = (value + 1).ToString();
            }
        }

        private void AddNewMatch_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}