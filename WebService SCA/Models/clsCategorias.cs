using Connection.SQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebService_SCA.Models
{
    public class clsCategorias
    {        
        public int idCategoria { get; set; }
        public string nomeCategoria { get; set; }

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
            SqlDataReader dr = SQLServer.ExecutarConsulta("select idCategoria,nomeCategoria from categorias");
            while (dr.Read())
            {
                retorno.Add(new clsCategorias(dr.GetInt32(0), dr.GetString(1)));
            }
            return retorno;
        }
    }
}