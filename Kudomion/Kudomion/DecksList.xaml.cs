using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database.Query;
using Kudomion.FirebaseManager;
using System.Diagnostics;

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecksList : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper(); 
       
        public DecksList()
        {
            InitializeComponent();

            //Initalize DeckList from Firebase!
            UpdateDecksList();
        }

       
        private async void UpdateDecksList()
        {
            //Load and assign to db retrived from Firebase.
             decksToLoad.ItemsSource = await firebaseHelper.GetAllDecks();
        }

       
        private async void AddDeckBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
            DeckItem newDeck = new DeckItem
            {
                title = dckName.Text, 
                thumbSrc = dckImg.Text, 
                ydkeCode = dckYdke.Text,
                ydkSrc = dckSrc.Text,
            };

            await firebaseHelper.AddDeck(newDeck);
            UpdateDecksList();

                return;

            }
            catch(Exception er)
            {
                Debug.WriteLine($"Error: {er}");
                return;
            }
        }

        private void GetYDKE(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DisplayAlert("Error!", $"Error: {button.Parent}", "OK!");
        }

        private void DownloadYDK (object sender, EventArgs e)
        {

        }
    }
}