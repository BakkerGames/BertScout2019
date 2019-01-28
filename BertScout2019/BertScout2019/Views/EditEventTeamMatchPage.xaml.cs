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
            SandstormMoveType = viewModel.item.SandstormMoveType;
            SandstormOffPlatform = viewModel.item.SandstormOffPlatform;
            SandstormHatches = viewModel.item.SandstormHatches;
            SandstormCargo = viewModel.item.SandstormCargo;

            CargoShipHatches = viewModel.item.CargoShipHatches;
            CargoShipCargo = viewModel.item.CargoShipCargo;
            RocketHatches = viewModel.item.RocketHatches;
            RocketCargo = viewModel.item.RocketCargo;
            RocketHighestHatch = viewModel.item.RocketHighestHatch;
            RocketHighestCargo = viewModel.item.RocketHighestCargo;

            EndgamePlatform = viewModel.item.EndgamePlatform;
            EndgameBuddyClimb = viewModel.item.EndgameBuddyClimb;

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
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_SandstormMoveType_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_SandstormMoveType_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_SandstormMoveType_None.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormMoveType_Auto.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormMoveType_Tele.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.SandstormMoveType != newValue)
                {
                    viewModel.item.SandstormMoveType = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_SandstormMoveType_None_Clicked(object sender, System.EventArgs e)
        {
            SandstormMoveType = 0;
        }

        private void Button_SandstormMoveType_Auto_Clicked(object sender, System.EventArgs e)
        {
            SandstormMoveType = 1;
        }

        private void Button_SandstormMoveType_Tele_Clicked(object sender, System.EventArgs e)
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
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_SandstormOffPlatform_None.BackgroundColor = App.SelectedButtonColor;
                        Button_SandstormOffPlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_SandstormOffPlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.SandstormOffPlatform != newValue)
                {
                    viewModel.item.SandstormOffPlatform = newValue;
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

        #region CargoShipHatches

        public int CargoShipHatches
        {
            get
            {
                return viewModel.item.CargoShipHatches;
            }
            set
            {
                Label_CargoShipHatches_Value.Text = value.ToString();
                if (viewModel.item.CargoShipHatches != value)
                {
                    viewModel.item.CargoShipHatches = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_CargoShipHatches_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (CargoShipHatches > 0)
            {
                CargoShipHatches--;
            }
        }

        private void Button_CargoShipHatches_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (CargoShipHatches < 99)
            {
                CargoShipHatches++;
            }
        }

        #endregion

        #region CargoShipCargo

        public int CargoShipCargo
        {
            get
            {
                return viewModel.item.CargoShipCargo;
            }
            set
            {
                Label_CargoShipCargo_Value.Text = value.ToString();
                if (viewModel.item.CargoShipCargo != value)
                {
                    viewModel.item.CargoShipCargo = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_CargoShipCargo_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (CargoShipCargo > 0)
            {
                CargoShipCargo--;
            }
        }

        private void Button_CargoShipCargo_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (CargoShipCargo < 99)
            {
                CargoShipCargo++;
            }
        }

        #endregion

        #region RocketHatches

        public int RocketHatches
        {
            get
            {
                return viewModel.item.RocketHatches;
            }
            set
            {
                Label_RocketHatches_Value.Text = value.ToString();
                if (viewModel.item.RocketHatches != value)
                {
                    viewModel.item.RocketHatches = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_RocketHatches_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHatches > 0)
            {
                RocketHatches--;
            }
        }

        private void Button_RocketHatches_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHatches < 99)
            {
                RocketHatches++;
            }
        }

        #endregion

        #region RocketCargo

        public int RocketCargo
        {
            get
            {
                return viewModel.item.RocketCargo;
            }
            set
            {
                Label_RocketCargo_Value.Text = value.ToString();
                if (viewModel.item.RocketCargo != value)
                {
                    viewModel.item.RocketCargo = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_RocketCargo_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketCargo > 0)
            {
                RocketCargo--;
            }
        }

        private void Button_RocketCargo_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketCargo < 99)
            {
                RocketCargo++;
            }
        }

        #endregion

        #region RocketHighestHatch

        public int RocketHighestHatch
        {
            get
            {
                return viewModel.item.RocketHighestHatch;
            }
            set
            {
                Label_RocketHighestHatch_Value.Text = value.ToString();
                if (viewModel.item.RocketHighestHatch != value)
                {
                    viewModel.item.RocketHighestHatch = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_RocketHighestHatch_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHighestHatch > 0)
            {
                RocketHighestHatch--;
            }
        }

        private void Button_RocketHighestHatch_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHighestHatch < 3)
            {
                RocketHighestHatch++;
            }
        }

        #endregion

        #region RocketHighestCargo

        public int RocketHighestCargo
        {
            get
            {
                return viewModel.item.RocketHighestCargo;
            }
            set
            {
                Label_RocketHighestCargo_Value.Text = value.ToString();
                if (viewModel.item.RocketHighestCargo != value)
                {
                    viewModel.item.RocketHighestCargo = value;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_RocketHighestCargo_Minus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHighestCargo > 0)
            {
                RocketHighestCargo--;
            }
        }

        private void Button_RocketHighestCargo_Plus_Clicked(object sender, System.EventArgs e)
        {
            if (RocketHighestCargo < 3)
            {
                RocketHighestCargo++;
            }
        }

        #endregion

        #region EndgamePlatform

        public int EndgamePlatform
        {
            get
            {
                return viewModel.item.EndgamePlatform;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_EndgamePlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_1.BackgroundColor = App.SelectedButtonColor;
                        Button_EndgamePlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_3.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_EndgamePlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_2.BackgroundColor = App.SelectedButtonColor;
                        Button_EndgamePlatform_3.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 3:
                        newValue = 3;
                        Button_EndgamePlatform_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_3.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_EndgamePlatform_None.BackgroundColor = App.SelectedButtonColor;
                        Button_EndgamePlatform_1.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_2.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgamePlatform_3.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.EndgamePlatform != newValue)
                {
                    viewModel.item.EndgamePlatform = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_EndgamePlatform_None_Clicked(object sender, System.EventArgs e)
        {
            EndgamePlatform = 0;
        }

        private void Button_EndgamePlatform_1_Clicked(object sender, System.EventArgs e)
        {
            EndgamePlatform = 1;
        }

        private void Button_EndgamePlatform_2_Clicked(object sender, System.EventArgs e)
        {
            EndgamePlatform = 2;
        }

        private void Button_EndgamePlatform_3_Clicked(object sender, System.EventArgs e)
        {
            EndgamePlatform = 3;
        }

        #endregion

        #region EndgameBuddyClimb

        public int EndgameBuddyClimb
        {
            get
            {
                return viewModel.item.EndgameBuddyClimb;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_EndgameBuddyClimb_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgameBuddyClimb_On.BackgroundColor = App.SelectedButtonColor;
                        Button_EndgameBuddyClimb_Lift.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_EndgameBuddyClimb_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgameBuddyClimb_On.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgameBuddyClimb_Lift.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_EndgameBuddyClimb_None.BackgroundColor = App.SelectedButtonColor;
                        Button_EndgameBuddyClimb_On.BackgroundColor = App.UnselectedButtonColor;
                        Button_EndgameBuddyClimb_Lift.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.EndgameBuddyClimb != newValue)
                {
                    viewModel.item.EndgameBuddyClimb = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_EndgameBuddyClimb_None_Clicked(object sender, System.EventArgs e)
        {
            EndgameBuddyClimb = 0;
        }

        private void Button_EndgameBuddyClimb_On_Clicked(object sender, System.EventArgs e)
        {
            EndgameBuddyClimb = 1;
        }

        private void Button_EndgameBuddyClimb_Lift_Clicked(object sender, System.EventArgs e)
        {
            EndgameBuddyClimb = 2;
        }

        #endregion
    }
}