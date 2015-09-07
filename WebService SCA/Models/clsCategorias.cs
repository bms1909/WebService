using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using Connection.MySQL;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Connection.SQLServer;

namespace WebService_SCA.Models
{
    public class clsCategorias
    {        
        public int idCategoria { get; set; }
        public string nomeCategoria { get; set; }
        //private static MySQL executor = new MySQL("b20f5b2179e89c", "96f6adfc", "br-cdbr-azure-south-a.cloudapp.net", "hefestobd");
        private static SQLServer executor = new SQLServer("hefestodbaws", "sanguedeunicornio", "hefestodb.ca93jazpypuz.sa-east-1.rds.amazonaws.com", "1433", "hefestodatabase", "false", "false", "30", "tcp");

        public clsCategorias()
        { }

        public clsCategorias(int id, string nome)
        {
            this.idCategoria = id;
            this.nomeCategoria = nome;
        }

        public static List<clsCategorias> carregaCategorias()
        {
            List<clsCategorias> retorno = new List<clsCategorias>();
            SqlDataReader dr = executor.ExecutarConsulta("select idCategoria,nomeCategoria from categorias");
            while (dr.Read())
            {
                retorno.Add(new clsCategorias(dr.GetInt32(0), dr.GetString(1)));
            }
            return retorno;
        }
    }
}