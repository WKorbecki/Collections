using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Collections.Models
{
    public class Item
    {
        public int IDItem { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Data dodania")]
        [Required]
        public DateTime DateAdd { get; set; }
        [Display(Name = "Czy publiczne")]
        [Required]
        public bool Publ { get; set; }
        [Display(Name = "Właściciel")]
        [Required]
        public int IDUser { get; set; }
        [Display(Name = "Typ")]
        [Required]
        public int IDType { get; set; }
        [Display(Name = "Lokalizacja")]
        [Required]
        public string Location { get; set; }
        [Display(Name = "Komentarz")]
        [Required]
        public string Comments { get; set; }

        public Item() : this(0, "", DateTime.Now.Date, true, 0, 0, "", "")
        {

        }

        public Item(int id, string name, DateTime date_add, bool publ, int user, int type, string location, string comments)
        {
            IDItem = id;
            Name = name;
            DateAdd = date_add;
            Publ = publ;
            IDUser = user;
            IDType = type;
            Location = location;
            Comments = comments;
        }

        public Item(MySqlDataReader dataReader) : this(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDateTime(2), dataReader.GetBoolean(3), dataReader.GetInt32(4), dataReader.GetInt32(5), dataReader.GetString(6), dataReader.GetString(7))
        {

        }
    }
}