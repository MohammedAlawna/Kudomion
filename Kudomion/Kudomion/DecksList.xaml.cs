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

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecksList : ContentPage
    {
        FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://kudomion-5c8e7-default-rtdb.firebaseio.com/");

        public DecksList()
        {

            InitializeComponent();
            //Create Dummy Deck:
          
          /*DeckItem dummyDeck = new DeckItem();
            dummyDeck.author = "KUDO";
            dummyDeck.thumbSrc = "https://www.phdgames.com/wp-content/uploads/2020/02/Yugi-Dino.jpg";
            dummyDeck.title = "Dinos";
            dummyDeck.ydkeCode = "qr53543ki2354"; 
            
            App.MyDatabase.CreateDeck(dummyDeck); */

            //Initialize The CollectionList:
            decksToLoad.ItemsSource = App.MyDatabase.ReadDecks();

        }

        public ObservableCollection<DeckItem> DeckItems { get; set; } = new ObservableCollection<DeckItem>();

        FirebaseClient firebaseClient2= new FirebaseClient("https://kudomion-5c8e7-default-rtdb.firebaseio.com/");
       
        //Function to test firebase!
        void ButtonClickedWrapper()
        {

            
        }
    }
}