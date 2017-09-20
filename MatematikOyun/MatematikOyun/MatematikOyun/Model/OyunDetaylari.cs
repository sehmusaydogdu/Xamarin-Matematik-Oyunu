using SQLite.Net.Attributes;
using System;


namespace MatematikOyun.Model
{
    public class OyunDetaylari
    {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public DateTime timeStart { get; set; }
            public uint Skor { get; set; }
     }
}
