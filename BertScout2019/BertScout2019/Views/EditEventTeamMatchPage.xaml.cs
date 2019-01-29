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

            Defense = viewModel.item.Defense;
            Cooperation = viewModel.item.Cooperation;
            Fouls = viewModel.item.Fouls;
            Broken = viewModel.item.Broken;

            AllianceResult = viewModel.item.AllianceResult;
            RocketRankingPoint = viewModel.item.RocketRankingPoint;
            HabRankingPoint = viewModel.item.HabRankingPoint;
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

        #region Defense

        public int Defense
        {
            get
            {
                return viewModel.item.Defense;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_Defense_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Defense_Some.BackgroundColor = App.SelectedButtonColor;
                        Button_Defense_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_Defense_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Defense_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Defense_Lots.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_Defense_None.BackgroundColor = App.SelectedButtonColor;
                        Button_Defense_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Defense_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.Defense != newValue)
                {
                    viewModel.item.Defense = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_Defense_None_Clicked(object sender, System.EventArgs e)
        {
            Defense = 0;
        }

        private void Button_Defense_Some_Clicked(object sender, System.EventArgs e)
        {
            Defense = 1;
        }

        private void Button_Defense_Lots_Clicked(object sender, System.EventArgs e)
        {
            Defense = 2;
        }

        #endregion

        #region Cooperation

        public int Cooperation
        {
            get
            {
                return viewModel.item.Cooperation;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_Cooperation_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Cooperation_Some.BackgroundColor = App.SelectedButtonColor;
                        Button_Cooperation_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_Cooperation_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Cooperation_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Cooperation_Lots.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_Cooperation_None.BackgroundColor = App.SelectedButtonColor;
                        Button_Cooperation_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Cooperation_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.Cooperation != newValue)
                {
                    viewModel.item.Cooperation = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_Cooperation_None_Clicked(object sender, System.EventArgs e)
        {
            Cooperation = 0;
        }

        private void Button_Cooperation_Some_Clicked(object sender, System.EventArgs e)
        {
            Cooperation = 1;
        }

        private void Button_Cooperation_Lots_Clicked(object sender, System.EventArgs e)
        {
            Cooperation = 2;
        }

        #endregion

        #region Fouls

        public int Fouls
        {
            get
            {
                return viewModel.item.Fouls;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_Fouls_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Fouls_Some.BackgroundColor = App.SelectedButtonColor;
                        Button_Fouls_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_Fouls_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Fouls_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Fouls_Lots.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_Fouls_None.BackgroundColor = App.SelectedButtonColor;
                        Button_Fouls_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Fouls_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.Fouls != newValue)
                {
                    viewModel.item.Fouls = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_Fouls_None_Clicked(object sender, System.EventArgs e)
        {
            Fouls = 0;
        }

        private void Button_Fouls_Some_Clicked(object sender, System.EventArgs e)
        {
            Fouls = 1;
        }

        private void Button_Fouls_Lots_Clicked(object sender, System.EventArgs e)
        {
            Fouls = 2;
        }

        #endregion

        #region Broken

        public int Broken
        {
            get
            {
                return viewModel.item.Broken;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_Broken_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Broken_Some.BackgroundColor = App.SelectedButtonColor;
                        Button_Broken_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_Broken_None.BackgroundColor = App.UnselectedButtonColor;
                        Button_Broken_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Broken_Lots.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_Broken_None.BackgroundColor = App.SelectedButtonColor;
                        Button_Broken_Some.BackgroundColor = App.UnselectedButtonColor;
                        Button_Broken_Lots.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.Broken != newValue)
                {
                    viewModel.item.Broken = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_Broken_None_Clicked(object sender, System.EventArgs e)
        {
            Broken = 0;
        }

        private void Button_Broken_Some_Clicked(object sender, System.EventArgs e)
        {
            Broken = 1;
        }

        private void Button_Broken_Lots_Clicked(object sender, System.EventArgs e)
        {
            Broken = 2;
        }

        #endregion

        #region AllianceResult

        public int AllianceResult
        {
            get
            {
                return viewModel.item.AllianceResult;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_AllianceResult_Lost.BackgroundColor = App.UnselectedButtonColor;
                        Button_AllianceResult_Tied.BackgroundColor = App.SelectedButtonColor;
                        Button_AllianceResult_Won.BackgroundColor = App.UnselectedButtonColor;
                        break;
                    case 2:
                        newValue = 2;
                        Button_AllianceResult_Lost.BackgroundColor = App.UnselectedButtonColor;
                        Button_AllianceResult_Tied.BackgroundColor = App.UnselectedButtonColor;
                        Button_AllianceResult_Won.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_AllianceResult_Lost.BackgroundColor = App.SelectedButtonColor;
                        Button_AllianceResult_Tied.BackgroundColor = App.UnselectedButtonColor;
                        Button_AllianceResult_Won.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.AllianceResult != newValue)
                {
                    viewModel.item.AllianceResult = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_AllianceResult_Lost_Clicked(object sender, System.EventArgs e)
        {
            AllianceResult = 0;
        }

        private void Button_AllianceResult_Tied_Clicked(object sender, System.EventArgs e)
        {
            AllianceResult = 1;
        }

        private void Button_AllianceResult_Won_Clicked(object sender, System.EventArgs e)
        {
            AllianceResult = 2;
        }

        #endregion

        #region RocketRankingPoint

        public int RocketRankingPoint
        {
            get
            {
                return viewModel.item.RocketRankingPoint;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_RocketRankingPoint_No.BackgroundColor = App.UnselectedButtonColor;
                        Button_RocketRankingPoint_Yes.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_RocketRankingPoint_No.BackgroundColor = App.SelectedButtonColor;
                        Button_RocketRankingPoint_Yes.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.RocketRankingPoint != newValue)
                {
                    viewModel.item.RocketRankingPoint = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_RocketRankingPoint_No_Clicked(object sender, System.EventArgs e)
        {
            RocketRankingPoint = 0;
        }

        private void Button_RocketRankingPoint_Yes_Clicked(object sender, System.EventArgs e)
        {
            RocketRankingPoint = 1;
        }

        #endregion

        #region HabRankingPoint

        public int HabRankingPoint
        {
            get
            {
                return viewModel.item.HabRankingPoint;
            }
            set
            {
                int newValue = value;
                switch (value)
                {
                    case 1:
                        newValue = 1;
                        Button_HabRankingPoint_No.BackgroundColor = App.UnselectedButtonColor;
                        Button_HabRankingPoint_Yes.BackgroundColor = App.SelectedButtonColor;
                        break;
                    default:
                        newValue = 0;
                        Button_HabRankingPoint_No.BackgroundColor = App.SelectedButtonColor;
                        Button_HabRankingPoint_Yes.BackgroundColor = App.UnselectedButtonColor;
                        break;
                }
                if (viewModel.item.HabRankingPoint != newValue)
                {
                    viewModel.item.HabRankingPoint = newValue;
                    App.database.SaveEventTeamMatchAsync(viewModel.item);
                }
            }
        }

        private void Button_HabRankingPoint_No_Clicked(object sender, System.EventArgs e)
        {
            HabRankingPoint = 0;
        }

        private void Button_HabRankingPoint_Yes_Clicked(object sender, System.EventArgs e)
        {
            HabRankingPoint = 1;
        }

        #endregion

        private void Match_Comments_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}