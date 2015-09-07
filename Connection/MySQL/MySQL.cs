using System;
using MySql.Data.MySqlClient;

namespace Connection.MySQL
{
    public class MySQL
    {
        private string DBusuario,DBsenha,DBservidor,DBnome;
        private MySqlConnection conn;
        public MySQL(string Usuario,string Senha,string Servidor,string NomeBanco)
        {
            this.DBusuario = Usuario;
            this.DBsenha = Senha;
            this.DBservidor = Servidor;
            this.DBnome = NomeBanco;
        }
        private MySqlConnection conectar()
        {
            conn = new MySqlConnection();
            conn.ConnectionString = "Server="+DBservidor+";Database="+DBnome+";Uid=" + DBusuario + ";Pwd=" + DBsenha + ";";
            conn.Open();
            return conn;
        }
        private void Desconectar()
        {
            conn.Close();
        }
        public bool ExecutarComando(string comando)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = conectar();
                command.CommandText = comando;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Desconectar();
            }
            return true;
        }
        public MySqlDataReader ExecutarConsulta(string comando)
        {
                MySqlDataReader rd;
                MySqlCommand command = new MySqlCommand();
                command.Connection = conectar();
                command.CommandText = comando;
                rd = command.ExecuteReader();
                return rd;
        }
        public bool ConfereBanco()
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
        }
    }
}
