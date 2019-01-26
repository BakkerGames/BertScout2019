using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventTeamMatchPage : ContentPage
    {
        public EditEventTeamMatchPage()
        {
            InitializeComponent();
            Title = $"Team {App.currTeamNumber} - Match {App.currMatchNumber}";
        }
    }
}