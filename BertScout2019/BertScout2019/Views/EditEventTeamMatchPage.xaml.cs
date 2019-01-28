using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEventTeamMatchPage : ContentPage
    {
        public EditEventTeamMatchPage()
        {
            InitializeComponent();
            Title = $"Team {App.currTeamNumber} - Match {App.currMatchNumber}";
        }

        #region SandstormMovementType

        private int _SandstormMovementType = 0;
        public int SandstormMovementType
        {
            get
            {
                return _SandstormMovementType;
            }
            set
            {
                Button_MovementType_None.BackgroundColor = App.UnselectedButtonColor;
                Button_MovementType_Tele.BackgroundColor = App.UnselectedButtonColor;
                Button_MovementType_Auto.BackgroundColor = App.UnselectedButtonColor;
                switch (value)
                {
                    case 0:
                        _SandstormMovementType = value;
                        Button_MovementType_None.BackgroundColor = App.SelectedButtonColor;
                        break;
                    case 1:
                        _SandstormMovementType = value;
                        Button_MovementType_Tele.BackgroundColor = App.SelectedButtonColor;
                        break;
                    case 2:
                        _SandstormMovementType = value;
                        Button_MovementType_Auto.BackgroundColor = App.SelectedButtonColor;
                        break;
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