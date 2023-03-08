using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudomion
{
    public class Room
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //First Player "p1" (p1: is the hoster as well).
        public string p1 { get; set; }

        public string p2 { get; set; }

        public bool status { get; set; }

        public bool disabled { get; set; }

        public string winner { get; set; }

    }
}
