using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecksList : ContentPage
    {
        public DecksList()
        {
            InitializeComponent();
            //Create Dummy Deck:
            DeckItem dummyDeck = new DeckItem();
            dummyDeck.author = "KUDO";
            dummyDeck.thumbSrc = "https://www.phdgames.com/wp-content/uploads/2020/02/Yugi-Dino.jpg";
            dummyDeck.title = "Dinos";
            dummyDeck.ydkeCode = "qr53543ki2354";
            
            App.MyDatabase.CreateDeck(dummyDeck);

            //Initialize The CollectionList:
            decksToLoad.ItemsSource = App.MyDatabase.ReadDecks();

            
        }
    }
}