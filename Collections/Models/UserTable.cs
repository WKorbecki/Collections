using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Collections.Models
{
    public class UsereTable
    {
        public static List<User> Get()
        {
            List<User> userList = new List<User>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM user";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while(sdr.Read())
                        {
                            userList.Add(new User(sdr));
                        }
                    }

                    con.Close();
                }
            }

            return userList;
        }

        public static User Get(int id)
        {
            User user = new User();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM user WHERE id_user = " + id;

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            user = new User(sdr);
                        }
                    }

                    con.Close();
                }
            }

            return user;
        }

        public static int Exist(User user)
        {
            int id = -1;
            int count = 0;

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM user WHERE email = " + user.Email + " AND password = " + user.Password;

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while(sdr.Read())
                        {
                            count++;
                            id = sdr.GetInt32(0);

                            if (count > 2)
                            {
                                id = -1;
                                break;
                            }
                        }
                    }

                    con.Close();
                }
            }

            return id;
        }
    }
}