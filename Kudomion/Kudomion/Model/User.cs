using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Kudomion
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public int posts { get; set; }

        public int duels { get; set; }
        public int ranking { get; set; }
        public int points { get; set; }
      
        public string usertype { get; set; }
        [MaxLength(17)]
        public string password { get; set; }
       
        public string NumberOfPosts
        {
            get
            {
                return posts.ToString();
            }
           
        }
    }
}
