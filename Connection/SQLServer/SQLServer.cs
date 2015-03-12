using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection.SQLServer
{
    public class SQLServer
    {
        public static string DBusuario = "bruno.musskopf@j5k19mz4oe", DBsenha = "$angueDeUn1cornio";
        public static SqlConnection conn { get; set; }
        private static SqlConnection conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Server=tcp:j5k19mz4oe.database.windows.net,1433;Database=scawsAkwE39MAGIu;User ID="+DBusuario+";Password="+DBsenha+";Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            conn.Open();
            return conn;
        }
        private static void Desconectar()
        {
            conn.Close();
        }
        public static bool ExecutarComando(string comando)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conectar();
                command.CommandText = comando;
                command.ExecuteNonQuery();
            }
            catch (Exception)
                {
                    return false;
                }
            finally
            {
                Desconectar();
            }
            return true;
        }
        public static SqlDataReader ExecutarConsulta(string comando)
        {
            SqlDataReader rd;
            SqlCommand command = new SqlCommand();
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
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
