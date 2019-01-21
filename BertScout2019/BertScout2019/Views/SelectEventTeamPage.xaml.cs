using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventTeamPage : ContentPage
    {
        SelectEventTeamsViewModel viewModel;

        public SelectEventTeamPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectEventTeamsViewModel();
        }

        private void EventTeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //todo
        }
    }
}