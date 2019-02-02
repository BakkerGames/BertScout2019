using BertScout2019.Models;
using BertScout2019.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetailsPage : ContentPage
    {
        Team team;

        TeamDetailViewModel viewModel;

        public TeamDetailsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TeamDetailViewModel();
        }



        protected override void OnAppearing()
        {
            TeamDetails_Number.Text = App.currTeamNumber.ToString();
            TeamDetails_Name.Text = App.currTeamName;
            TeamDetails_Location.Text = "anytownusa";
        }
    }
}