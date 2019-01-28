using System;
using BertScout2019.Models;
using BertScout2019.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventTeamMatchPage : ContentPage
    {
        EditEventTeamMatchViewModel viewModel;

        public EditEventTeamMatchPage(EventTeamMatch item)
        {
            InitializeComponent();

            BindingContext = viewModel = new EditEventTeamMatchViewModel(item);

            SetButtons();
        }

        private void SetButtons()
        {
            SandstormMovementType = viewModel.item.SandstormMoveType;

            //todo add more here
        }

        #region SandstormMovementType

        public int SandstormMovementType
        {
            get
            {
                return viewModel.item.SandstormMoveType;
            }
            set
            {
                int saveValue = viewModel.item.SandstormMoveType;
                Button_MovementType_None.BackgroundColor = App.UnselectedButtonColor;
                Button_MovementType_Tele.BackgroundColor = App.UnselectedButtonColor;
                Button_MovementType_Auto.BackgroundColor = App.UnselectedButtonColor;
                switch (value)
                {
                    case 1:
                        viewModel.item.SandstormMoveType = 1;
                        Button_MovementType_Tele.BackgroundColor = App.SelectedButtonColor;
                        break;
                    case 2:
                        viewModel.item.SandstormMoveType = 2;
                        Button_MovementType_Auto.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        viewModel.item.SandstormMoveType = 0;
                        Button_MovementType_None.BackgroundColor = App.SelectedButtonColor;
                        break;
                }
                if (saveValue != viewModel.item.SandstormMoveType)
                {
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_MovementType_None_Clicked(object sender, System.EventArgs e)
        {
            SandstormMovementType = 0;
        }

        private void Button_MovementType_Tele_Clicked(object sender, System.EventArgs e)
        {
            SandstormMovementType = 1;
        }

        private void Button_MovementType_Auto_Clicked(object sender, System.EventArgs e)
        {
            SandstormMovementType = 2;
        }

        #endregion
    }
}