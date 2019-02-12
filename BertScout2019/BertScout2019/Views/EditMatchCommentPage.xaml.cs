using BertScout2019.ViewModels;
using BertScout2019Data.Models;
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
            Editor_MatchScouterName.Text = viewModel.item.ScouterName;
            Editor_MatchComment.Text = viewModel.item.Comments;
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
            try
            {
                if (viewModel.item.ScouterName != Editor_MatchScouterName.Text
                    || viewModel.item.Comments != Editor_MatchComment.Text)
                {
                    viewModel.item.ScouterName = Editor_MatchScouterName.Text.Trim();
                    viewModel.item.Comments = Editor_MatchComment.Text;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
                ErrorMessage.Text = "Saved!";
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        private void ToolbarItem_Save_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (viewModel.item.ScouterName != Editor_MatchScouterName.Text
                    || viewModel.item.Comments != Editor_MatchComment.Text)
                {
                    viewModel.item.ScouterName = Editor_MatchScouterName.Text.Trim();
                    viewModel.item.Comments = Editor_MatchComment.Text;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
                ErrorMessage.Text = "Saved!";
            }
            catch (System.Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        private void Editor_MatchComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorMessage.Text = "";
        }

        private string SaveName = "";

        private void Editor_MatchScouterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorMessage.Text = "";
            Editor_MatchComment.IsEnabled = (Editor_MatchScouterName.Text?.Length >= 3);
            /*if (SaveName == "" && Editor_MatchScouterName.Text?.Length >= 3)
            {
                SaveName = Editor_MatchScouterName.Text;
            }

            else if (SaveName != "" && Editor_MatchScouterName.Text?.Length > 3)
            {
                Editor_MatchScouterName.Text = SaveName;
            }*/
        }
    }
}