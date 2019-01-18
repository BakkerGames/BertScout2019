using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FRCEventDetailPage : ContentPage
    {
        FRCEventDetailViewModel viewModel;

        public FRCEventDetailPage(FRCEventDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public FRCEventDetailPage()
        {
            InitializeComponent();

            var item = new FRCEvent
            {
                Name = "Event 1 Name",
                Location = "Event 1 Location"
            };

            viewModel = new FRCEventDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}