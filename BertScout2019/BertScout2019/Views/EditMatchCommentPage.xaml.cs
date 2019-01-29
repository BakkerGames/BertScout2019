using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMatchCommentPage : ContentPage
    {
        EditEventTeamMatchViewModel viewModel;

        public EditMatchCommentPage(EventTeamMatch item)
        {
            InitializeComponent();
            BindingContext = viewModel = new EditEventTeamMatchViewModel(item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(viewModel.item.Comments))
            {
                viewModel.item.Comments = "Name: ";
            }
            Editor_MatchComment.Text = viewModel.item.Comments;
            Editor_MatchComment.Focus();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (!viewModel.item.Comments.Equals(Editor_MatchComment.Text))
            {
                viewModel.item.Comments = Editor_MatchComment.Text;
                App.database.SaveEventTeamMatchAsync(viewModel.item);
            }
        }

        private void ToolbarItem_Save_Clicked(object sender, System.EventArgs e)
        {
            if (!viewModel.item.Comments.Equals(Editor_MatchComment.Text))
            {
                viewModel.item.Comments = Editor_MatchComment.Text;
                App.database.SaveEventTeamMatchAsync(viewModel.item);
            }
        }
    }
}