using BertScout2019.Data;
using BertScout2019.Models;
using BertScout2019.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2019.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        public IDataStore<FRCEvent> DataStoreFRCEvent => new XmlDataStoreFRCEvent();
        public BertScout2019Database database;

        public OptionsPage()
        {
            InitializeComponent();
        }

        public void Fill_Database_Button_Clicked(object sender, EventArgs e)
        {
            string saveText = Fill_Database_Button.Text;
            Fill_Database_Button.Text = "Filling...";
            App app = Application.Current as App;
            database = new BertScout2019Database(app.dbPath);
            var items = DataStoreFRCEvent.GetItemsAsync(true).Result;
            foreach (var item in items)
            {
                int result = database.SaveFRCEventAsync(item).Result;
            }
            Fill_Database_Button.Text = saveText;
        }
    }
}