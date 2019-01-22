using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectEventPage : ContentPage
    {
        SelectEventsViewModel viewModel;

        public SelectEventPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectEventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.FRCEvents.Count == 0)
                viewModel.LoadFRCEventsCommand.Execute(null);

            App app = Application.Current as App;

            foreach (FRCEvent item in viewModel.FRCEvents)
            {
                if (item.EventKey == app.currFRCEventKey)
                {
                    FRCEventsListView.SelectedItem = item;
                    break;
                }
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as FRCEvent;
            if (item == null)
                return;

            App app = Application.Current as App;
            app.currFRCEventKey = item.EventKey;
            app.currFRCEventName = item.Name;
        }
    }
}