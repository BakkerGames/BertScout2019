using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectTeamMatchPage : ContentPage
    {
        SelectEventTeamMatchesViewModel viewModel;

        public SelectTeamMatchPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SelectEventTeamMatchesViewModel();
        }
    }
}