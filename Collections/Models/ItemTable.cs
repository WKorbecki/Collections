using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace Collections.Models
{
    public class ItemTable
    {
        public static List<Item> Get(int id_user)
        {
            List<Item> itemList = new List<Item>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM item WHERE id_user = " + id_user + " OR public = 1";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while(sdr.Read())
                        {
                            itemList.Add(new Item(sdr));
                        }
                    }

                    con.Close();
                }
            }

            return itemList;
        }

        public static Item Get(int id_item, int id_user = -1)
        {
            Item item = new Item();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM item WHERE id_item = " + id_item + " AND ( id_user = " + id_user + " OR public = 1 )";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            item = new Item(sdr);
                        }
                    }

                    con.Close();
                }
            }

            return item;
        }

        public static void Input(Item item)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO item (name, date_add, public, id_user, id_type, location, comments) VALUES (@name, @date_add, @public, @id_user, @id_type, @location, @comments)";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@date_add", item.DateAdd);
                    cmd.Parameters.AddWithValue("@public", item.Publ);
                    cmd.Parameters.AddWithValue("@id_user", item.IDUser);
                    cmd.Parameters.AddWithValue("@id_type", item.IDType);
                    cmd.Parameters.AddWithValue("@location", item.Location);
                    cmd.Parameters.AddWithValue("@comments", item.Comments);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void Update(Item item)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "UPDATE item SET name = @name, date_add = @date_add, public = @public, id_user = @id_user, id_type = @id_type, location = @location, comments = @comments WHERE id_item = @id_item";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.Parameters.AddWithValue("@id_item", item.IDItem);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@date_add", item.DateAdd);
                    cmd.Parameters.AddWithValue("@public", item.Publ);
                    cmd.Parameters.AddWithValue("@id_user", item.IDUser);
                    cmd.Parameters.AddWithValue("@id_type", item.IDType);
                    cmd.Parameters.AddWithValue("@location", item.Location);
                    cmd.Parameters.AddWithValue("@comments", item.Comments);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static void Delete(Item item)
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "DELETE FROM item WHERE id_item = @id_item";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();

                    cmd.Parameters.AddWithValue("@id_item", item.IDItem);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}