using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Collections.Models
{
    public class Type
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Type() : this(0, "")
        {

        }

        public Type(MySqlDataReader dataReader) : this(dataReader.GetInt32(0), dataReader.GetString(1))
        {

        }

        public Type(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}