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
	public partial class AddNewTeamPage : ContentPage
	{
		public AddNewTeamPage ()
		{
			InitializeComponent ();
		}

        private void Add_New_Team_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_NewTeam_Clicked(object sender, EventArgs e)
        {
            /*int value = int.Parse(TeamNumberLabelValue.Text);
            foreach (TeamNumber oldTeam in viewModel.Teams)
            {
                if (oldTeam.TeamNumber == value)
                {
                    return;
                }
            }
            TeamNumber newTeam = new TeamNumber();
            newTeam.EventKey = App.currFRCEventKey;
            newTeam.TeamNumber = App.currTeamNumber;
            App.database.SaveTeamNumberAsync(newTeam);
            if (App.highestTeamNumber < value)
            {
                App.highestTeamNumber = value;
            }
            // add new team into list in proper order
            bool found = false;
            for (int i = 0; i < viewModel.Teams.Count; i++)
            {
                if (viewModel.Teams[i].TeamNumber > value)
                {
                    found = true;
                    viewModel.Teams.Insert(i, newTeam);
                    break;
                }
            }
            if (!found)
            {
                viewModel.TeamNumber.Add(newTeam);
            }*/
        }
    }
}