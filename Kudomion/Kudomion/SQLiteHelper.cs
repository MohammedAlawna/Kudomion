using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
namespace Kudomion
{
    public class SQLiteHelper
    {
        private readonly SQLiteConnection db;
        
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<User>();
            db.CreateTable<Post>();
            db.CreateTable<Room>();
            db.CreateTable<DeckItem>();
        }


        public int CreateDeck(DeckItem itemToAdd)
        {
            return db.Insert(itemToAdd);
        }


        public List<DeckItem> ReadDecks()
        {
            return db.Table<DeckItem>().ToList();
        }


        public int CreateRoom(Room room)
        {
           
            return db.Insert(room);
        }


        public List<Room> ReadRooms()
        {
            return db.Table<Room>().ToList();
        }


        public int DeleteRoom(DeckItem itemToAdd)
        {
            return db.Delete(itemToAdd);
        }


        //Show The Room Not Finishied and for the second player.
         public List<Room> GetActiveRoom(string nameOfUser)
         {
            //Get Room From DB where nameOfUser == p2 && status == false;
            var tableQuery = db.Table<Room>();
            // List<Room> activeRoom = tableQuery.Where(x => x.p2 == nameOfUser && x.status == false).ToList();
           
            List<Room> activeRoom = tableQuery.Where(x=> x.disabled != true && x.status == false).ToList();

            return activeRoom;
        }

       
        public int DecideWinner(Room room, User winner){
            room.status = true;
            room.winner = winner.name;
            IncreasePoints(winner, 3);
            return db.Update(room);
        }

        int IncreasePoints(User winner, int amount)
        {
            winner.points += amount;
           
            return db.Update(winner);
            
        }

     

        public List<string> ReadAllActiveRoomsToString()
        {
            var getAllRooms = db.Table<Room>().Where(x => x.status == false).ToList();
            List<string> stringsList = new List<string>();

            foreach(Room r in getAllRooms)
            {
                stringsList.Add(r.p2);
            }
            for (int i = 0; i < stringsList.Count; i++)
            {
                Console.WriteLine("List of Active Rooms Are: " + stringsList[i]);
            }
            return stringsList;
        }
        public List<string> ReadAllRoomsToString()
       {
            //Get all rooms in a list and add the name of the players into a list.
            var getAllRooms = db.Table<Room>().ToList();
            List<string> stringsList = new List<string>();

            foreach(Room r in getAllRooms)
            {
                stringsList.Add(r.p2);
            }

            //for(int i = 0; i < stringsList.Count; i++)
            //{
            //    Console.WriteLine("List of Rooms Are: " + stringsList[i]);
            //}
           
            return stringsList;
           
       }

        public int CreateUser(User user)
        {
            return db.Insert(user);
        }

        public List<User> ReadUsers()
        {
            return db.Table<User>().ToList();
        }


        public int UpdateUser(User userToUpdate)
        {
            userToUpdate.posts = userToUpdate.posts + 1;
            // Home.noPosts.Text = userToUpdate.posts.ToString();
            Home.updateUser();
         //   instance.UpdatePostProfile();
            return db.Update(userToUpdate);
            
        }


        public int DeleteUser(User userToDelete)
        {
            return db.Delete(userToDelete);
        }


        public User LoginValidate(string userName1, string pwd1)
        {
            var tableQuery = db.Table<User>();
            User userCheck = tableQuery.Where(x => x.name == userName1 && x.password == pwd1).FirstOrDefault();

            return userCheck;
        }


        //Function to get Specific User from the username only.
        public User getSpecificUser(string userToGet)
        {
            var tableQuery = db.Table<User>();
            User userCheck = tableQuery.Where(u => u.name == userToGet).FirstOrDefault();
            return userCheck;
        }

        
        //Functions For Posting System.
        public int CreatePost(Post postToCreate)
        {
           Home.updateUser();
           return db.Insert(postToCreate);
           
        }


        public List<Post> ReadPosts()
        {
            return db.Table<Post>().ToList();
        }

       

        public int DeletePost(Post postToDelete)
        {
            return db.Delete(postToDelete);
        }


       
    }
}
