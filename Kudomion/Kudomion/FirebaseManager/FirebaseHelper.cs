using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudomion.FirebaseManager
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://kudomion-5c8e7-default-rtdb.firebaseio.com/");

        public async Task<List<DeckItem>> GetAllDecks()
        {
            return (await firebaseClient.Child("Decks").OnceAsync<DeckItem>()).Select(item=> new DeckItem { 
            title = "BlueEyes",
            thumbSrc = "https://mktg-assets.tcgplayer.com/filters:watermark(tcgplayer-cdn-prd,infinite-watermark.png,-30,-80,40)//content/yugioh/2022/Feb/THE%20BEST%20Blue-Eyes%20White%20Dragon%20Deck%20In%20Yu-Gi-Oh.jpg"
            }).ToList();
        }
    }
}
