using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Collections.Models
{
    public class User
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Hasło")]
        [Required]
        public string Password { get; set; }

        public User()
        {

        }

        public User(MySqlDataReader dataReader) : this(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3))
        {

        }

        public User(int id, string name, string email, string password)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public bool SignIn(User user)
        {
            return (Email.Equals(user.Email) && Password.Equals(user.Password));
        }

        public bool SignIn(List<User> userList)
        {
            foreach(var user in userList)
            {
                if (SignIn(user))
                    return true;
            }

            return false;
        }
    }
}