using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProFind.Lib.Global.Helpers
{
    internal class Connection
    {
        public string usuario { get; set; }
        public string servidor { get; set; }
        public string bd { get; set; }

        public string cadena { get; set; }

        public string port { get; set; }
        public string pass { get; set; }

        public static MySqlConnection con { get; set; }

        public Connection()
        {
            usuario = "root";
            servidor = "127.0.0.1";
            bd = "ProFind";
            port = "3306";

            cadena = $"server={servidor};uid={usuario};database={bd};port={port};";


            con = new MySqlConnection(cadena);
        }

        public MySqlConnection Conectar()
        {
            return con;
        }

        public static DataTable ExecuteQueryInDatabase(string sentence)
        {
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter(sentence, con);

            adapter.Fill(dt);

            return dt;
        }

        public static int ExecuteNonQueryInDatabase(string sentence)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sentence, con);
                con.Open();
                int numberOf = cmd.ExecuteNonQuery();
                return numberOf;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
    }



}