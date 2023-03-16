using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudomion.FirebaseManager
{
    public class FirebaseHelper
    {
        //Follow this Article: https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-realtime-database-crud-operations/

        FirebaseClient firebaseClient = new FirebaseClient("https://kudomion-5c8e7-default-rtdb.firebaseio.com/");
     
        public async Task<List<DeckItem>> GetAllDecks()
        {

            return (await firebaseClient
              .Child("Decks")
              .OnceAsync<DeckItem>()).Select(item => new DeckItem
              {
                  title = item.Object.title,
                  thumbSrc = item.Object.thumbSrc,
              }).ToList();
        }

        public async Task AddPerson(string _title, string _thumbSrc)
        {
            DeckItem newItem = new DeckItem() {title = _title, thumbSrc = _thumbSrc };
            await firebaseClient.Child("Decks").PostAsync(newItem);
        }

    
    }
}
