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

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}

        private void SetButtons()
        {
            SandstormMoveType = viewModel.item.SandstormMoveType;
            SandstormOffPlatform = viewModel.item.SandstormOffPlatform;
            SandstormHatches = viewModel.item.SandstormHatches;
            SandstormCargo = viewModel.item.SandstormCargo;

            //todo add more here
        }

        #region SandstormMoveType

        public int SandstormMoveType
        {
            get
            {
                return viewModel.item.SandstormMoveType;
            }
            set
            {
                int saveValue = viewModel.item.SandstormMoveType;
                switch (value)
                {
                    case 1:
                        viewModel.item.SandstormMoveType = 1;
                        Button_SandstormMoveType_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        viewModel.item.SandstormMoveType = 2;
                        Button_SandstormMoveType_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        viewModel.item.SandstormMoveType = 0;
                        Button_SandstormMoveType_None.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (saveValue != viewModel.item.SandstormMoveType)
                {
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_SandstormMoveType_None_Clicked(object sender, System.EventArgs e)
        {
            SandstormMoveType = 0;
        }

        private void Button_SandstormMoveType_Tele_Clicked(object sender, System.EventArgs e)
        {
            SandstormMoveType = 1;
        }

        private void Button_SandstormMoveType_Auto_Clicked(object sender, System.EventArgs e)
        {
            SandstormMoveType = 2;
        }

        #endregion

        #region SandstormOffPlatform

        public int SandstormOffPlatform
        {
            get
            {
                return viewModel.item.SandstormOffPlatform;
            }
            set
            {
                int saveValue = viewModel.item.SandstormOffPlatform;
                switch (value)
                {
                    case 1:
                        viewModel.item.SandstormOffPlatform = 1;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        viewModel.item.SandstormOffPlatform = 2;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        viewModel.item.SandstormOffPlatform = 0;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (saveValue != viewModel.item.SandstormOffPlatform)
                {
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_SandstormOffPlatform_None_Clicked(object sender, System.EventArgs e)
        {
            SandstormOffPlatform = 0;
        }

        private void Button_SandstormOffPlatform_1_Clicked(object sender, System.EventArgs e)
        {
            SandstormOffPlatform = 1;
        }

        private void Button_SandstormOffPlatform_2_Clicked(object sender, System.EventArgs e)
        {
            SandstormOffPlatform = 2;
        }

        #endregion

        #region SandstormHatches

        public int SandstormHatches
        {
            get
            {
                return viewModel.item.SandstormHatches;
            }
            set
            {
                Label_SandstormHatches_Value.Text = value.ToString();
                if (viewModel.item.SandstormHatches != value)
                {
                    viewModel.item.SandstormHatches = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_SandstormHatches_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (SandstormHatches > 0)
            {
                SandstormHatches--;
            }
        }

        private void Button_SandstormHatches_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (SandstormHatches < 99)
            {
                SandstormHatches++;
            }
        }

        #endregion

        #region SandstormCargo

        public int SandstormCargo
        {
            get
            {
                return viewModel.item.SandstormCargo;
            }
            set
            {
                Label_SandstormCargo_Value.Text = value.ToString();
                if (viewModel.item.SandstormCargo != value)
                {
                    viewModel.item.SandstormCargo = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_SandstormCargo_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (SandstormCargo > 0)
            {
                SandstormCargo--;
            }
        }

        private void Button_SandstormCargo_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (SandstormCargo < 99)
            {
                SandstormCargo++;
            }
        }

        #endregion
    }
}