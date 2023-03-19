using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudomion.FirebaseManager
{
    public class FirebaseHelper
    {
        //Follow this Article: https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-realtime-database-crud-operations/

        FirebaseClient firebaseClient = new FirebaseClient("https://kudomion-5c8e7-default-rtdb.firebaseio.com/");
     

        //Start User-Related Functions.
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var usersList = (await firebaseClient.Child("Users").OnceAsync<User>()).Select(item =>
                new User
                {
                    name = item.Object.name,
                    password = item.Object.password,
                    duels = item.Object.duels, 
                    points = item.Object.points,
                    ranking = item.Object.ranking,
                    usertype = item.Object.usertype,
                    posts = item.Object.posts,
                }).ToList();
                return usersList;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return null;
            }
        }

        public async Task<User> GetUserByName(string name)
        {
            try
            {
                var allUsers = await GetAllUsers();
                await firebaseClient.Child("Users").OnceAsync<User>();
                return allUsers.Where(a => a.name == name).FirstOrDefault();
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return null;
            }
        }

        //Get User By Email (To Be Implemented Later)
        /*public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var allUsers = await GetAllUsers();
                await firebaseClient.Child("Users").OnceAsync<User>();
                return allUsers.Where(a => a.name == name)
            }
            catch (Exception e)
            {

            }
        }*/

        public async Task<bool> AddUser(string _name, string _password)
        {
            try
            {
                await firebaseClient.Child("Users").PostAsync(new User() { name = _name, password = _password, points = 0, posts = 0, ranking = 0, usertype = "USER" });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(string _name, string _password)
        {
            try
            {
                var userToUpdate = (await firebaseClient.Child("Users").OnceAsync<User>()).Where(a => a.Object.name == _name).FirstOrDefault();

                await firebaseClient.Child("Users").Child(userToUpdate.Key).PutAsync(new User() { name = _name, password = _password });
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(string _name)
        {
            try
            {
                var toDeletePerson = (await firebaseClient.Child("Users").OnceAsync<User>()).Where(a => a.Object.name == _name).FirstOrDefault();
                await firebaseClient.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error: {e}");
                return false;
            }
        }


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

        public async Task AddDeck(string _title, string _thumbSrc)
        {
            DeckItem newItem = new DeckItem() {title = _title, thumbSrc = _thumbSrc };
            await firebaseClient.Child("Decks").PostAsync(newItem);
        }

    
    }
}
