using BertScout2019.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewFRCEventPage : ContentPage
    {
        public FRCEvent FRCEvent { get; set; }

        public NewFRCEventPage()
        {
            InitializeComponent();

            FRCEvent = new FRCEvent
            {
                Name = "FRC Event name",
                Location = "FRC Event location"
            };

            BindingContext = this;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddFRCEvent", FRCEvent);
            Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}