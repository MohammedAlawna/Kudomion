using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Kudomion
{
    public class DeckItem
    {
   
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public string thumbSrc { get; set; }
        public string ydkeCode { get; set; }
        public string ydkSrc { get; set; }


       
    }
}
