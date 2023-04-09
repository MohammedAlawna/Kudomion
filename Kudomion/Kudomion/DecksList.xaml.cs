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


            //Create Dummy Deck:
          
          /*DeckItem dummyDeck = new DeckItem();
            dummyDeck.author = "KUDO";
            dummyDeck.thumbSrc = "https://www.phdgames.com/wp-content/uploads/2020/02/Yugi-Dino.jpg";
            dummyDeck.title = "Dinos";
            dummyDeck.ydkeCode = "qr53543ki2354"; 
            
            App.MyDatabase.CreateDeck(dummyDeck); */

            //Initialize The CollectionList:
          //  decksToLoad.ItemsSource = App.MyDatabase.ReadDecks();

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
    }
}