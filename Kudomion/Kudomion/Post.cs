using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudomion
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string author { get; set; }
        public string content { get; set; }

        //Image if exist and when it's implemented.
        public string imagePath { get; set; }

        //Comments will be implemented later on.
        //public string[] comments { get; set; }

        //Name of users who added "good" impression..
        //Number of "goods" added by getting the count of this array.
       // public string[] goods { get; set; }
        

    }
}
