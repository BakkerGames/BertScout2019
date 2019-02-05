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
            int newTeamNumber = 0;
            if (Add_New_Team.Text == "")
            {
                this.Title = "Must Specify Team Number";
                return;
            }
            if (!int.TryParse(Add_New_Team.Text, out newTeamNumber))
            {
                this.Title = "Numbers Only Please!";
                return;
            }
            if (newTeamNumber > 9999 || newTeamNumber < 1)
            {
                this.Title = "Number out of range";
                return;
            }
            //if (App.database.GetTeamsAsync(newTeamNumber))
            //{
            //    this.Title = "Team Already Exists";
            //    return;
            //}
                this.Title = $"Added new team {newTeamNumber}";
        }
    }
}