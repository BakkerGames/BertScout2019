using BertScout2019.ViewModels;
using BertScout2019Data.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditMatchCommentPage : ContentPage
    {
        bool _loadingFlag = false;
        EditEventTeamMatchViewModel viewModel;

        public EditMatchCommentPage(EventTeamMatch item)
        {
            InitializeComponent();

            if (item.Changed % 2 == 0) // change from even to odd, odd = must upload
            {
                item.Changed++;
            }

            BindingContext = viewModel = new EditEventTeamMatchViewModel(item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _loadingFlag = true;
            Editor_MatchScouterName.Text = viewModel.item.ScouterName ?? "";
            Editor_MatchComment.Text = viewModel.item.Comments ?? "";
            if (string.IsNullOrEmpty(Editor_MatchScouterName.Text?.Trim()))
            {
                ErrorMessage.Text = "Please enter your name";
            }
            else
            {
                ErrorMessage.Text = "";
            }
            _loadingFlag = false;
            if (string.IsNullOrEmpty(Editor_MatchScouterName.Text))
            {
                Editor_MatchScouterName.Focus();
            }
            else
            {
                Editor_MatchComment.Focus();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SaveComments();
        }

        private void ToolbarItem_Save_Clicked(object sender, System.EventArgs e)
        {
            SaveComments();
        }

        private void SaveComments()
        {
            try
            {
                if (viewModel.item.ScouterName != Editor_MatchScouterName.Text
                    || viewModel.item.Comments != Editor_MatchComment.Text)
                {
                    viewModel.item.ScouterName = Editor_MatchScouterName.Text?.Trim();
                    viewModel.item.Comments = Editor_MatchComment.Text?.Trim();
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
                if (string.IsNullOrEmpty(Editor_MatchScouterName.Text?.Trim()))
                {
                    ErrorMessage.Text = "Please enter your name";
                }
                else
                {
                    ErrorMessage.Text = "";
                }
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        private void Editor_MatchComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_loadingFlag && string.IsNullOrEmpty(ErrorMessage.Text))
            {
                ErrorMessage.Text = "(Not Saved)";
            }
        }

        private void Editor_MatchScouterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_loadingFlag && string.IsNullOrEmpty(ErrorMessage.Text))
            {
                ErrorMessage.Text = "(Not Saved)";
            }
        }
    }
}
