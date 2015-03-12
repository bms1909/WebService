using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

namespace Connection.MySQL
{
    public class MySQL
    {
        /*public static string DBusuario="root",DBsenha="";
        public static MySqlConnection conn {get;set;}
        private static MySqlConnection conectar()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "Server=localhost;Database=biofitness;Uid=" + DBusuario + ";Pwd=" + DBsenha + ";";
            conn.Open();
            return conn;
        }
        private static void Desconectar()
        {
            conn.Close();
        }
        public static void ExecutarComando(string comando)
        {
                MySqlCommand command = new MySqlCommand();
                command.Connection = conectar();
                command.CommandText = comando;
                command.ExecuteNonQuery();
                Desconectar();
        }
        public static MySqlDataReader ExecutarConsulta(string comando)
        {
                MySqlDataReader rd;
                MySqlCommand command = new MySqlCommand();
                command.Connection = conectar();
                command.CommandText = comando;
                rd = command.ExecuteReader();
            return rd;
        }
        public static bool ConfereBanco()
        {
            try
            {
                    conectar();
                    return true;
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                Desconectar();
            }
        }*/
    }
}
