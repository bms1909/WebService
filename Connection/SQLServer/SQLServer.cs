using System;
using System.Data.SqlClient;

namespace Connection.SQLServer
{
    public class SQLServer
    {
        private string DBusuario, DBsenha, DBservidor, DBporta, DBnome, 
            DBconexaoConfiada= "False",DBcriptografada="True", DBtimeout="30",DBtipoPorta="tcp";
        private SqlConnection conn;
        /// <summary>
        /// Cria um objeto de conexão com parâmetros avançados pré-configurados (trustedConnection=false, Criptografia=true, Timeout=30 e porta=tcp)
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Senha"></param>
        /// <param name="Endereco do Servidor"></param>
        /// <param name="Porta"></param>
        /// <param name="Nome da Base"></param>
        public SQLServer (string Usuario,string Senha, string EnderecoServidor, string Porta, string NomeBase)
        {
            this.DBusuario = Usuario;
            this.DBsenha = Senha;
            this.DBservidor = EnderecoServidor;
            this.DBporta = Porta;
            this.DBnome = NomeBase;
        }
        /// <summary>
        /// Cria um objeto de conexão ao banco de dados com parâmetros avançados
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Senha"></param>
        /// <param name="Endereco do Servidor"></param>
        /// <param name="Porta"></param>
        /// <param name="Nome da Base"></param>
        /// <param name="TrustedConnection"></param>
        /// <param name="ConexaoCriptografada"></param>
        /// <param name="Timeout"></param>
        /// <param name="Tipo de Porta"></param>
        public SQLServer(string Usuario, string Senha, string EnderecoServidor, string Porta, string NomeBase,string TrustedConnection, string ConexaoCriptografada, string Timeout,string TipoPorta)
        {
            this.DBusuario = Usuario;
            this.DBsenha = Senha;
            this.DBservidor = EnderecoServidor;
            this.DBporta = Porta;
            this.DBnome = NomeBase;
            this.DBconexaoConfiada = TrustedConnection;
            this.DBcriptografada = ConexaoCriptografada;
            this.DBtimeout = Timeout;
            this.DBtipoPorta = TipoPorta;
        }


        private SqlConnection conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Server="+DBtipoPorta+":"+DBservidor+","+DBporta+";Database="+DBnome+";User ID="+DBusuario+";Password="+DBsenha+";Trusted_Connection="+DBconexaoConfiada+";Encrypt="+DBcriptografada+";Connection Timeout="+DBtimeout+";";
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
        public SqlDataReader ExecutarConsulta(string comando)
        {
            SqlDataReader rd;
            SqlCommand command = new SqlCommand();
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
