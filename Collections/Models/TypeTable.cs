using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Collections.Models
{
    public class TypeTable
    {
        public static List<Type> Get()
        {
            List<Type> typeList = new List<Type>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM type";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while(sdr.Read())
                        {
                            typeList.Add(new Type(sdr));
                        }
                    }

                    con.Close();
                }
            }

            return typeList;
        }

        public static Type Get(int id)
        {
            Type type = new Type();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM type WHERE id_type = " + id;

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        type = new Type(sdr);
                    }

                    con.Close();
                }
            }

            return type;
        }
    }
}