// For dropdown menu-use picker, info at https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/picker/

using System;
using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultPage : ContentPage
        
	{
        SelectTeamsByEventViewModel viewModel;

        public ResultPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new SelectTeamsByEventViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //EventTeamsListView.SelectedItem = null;
        }

        private async void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Team item = (Team)args.SelectedItem;
            if (item == null)
            {
                return;
            }
            App.currTeamNumber = item.TeamNumber;
            App.currTeamName = item.Name;
            await Navigation.PushAsync(new SelectEventTeamMatchPage(App.currFRCEventKey, item));
        }

        private void RP_Clicked(object sender, EventArgs e)
        {

        }

        private void AvgScore_Clicked(object sender, EventArgs e)
        {

        }
    }
}