using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connection.SQLServer;
using System.Data.SqlClient;
namespace WebService_SCA.Models
{
    public class clsUsuarios
    {
        public int idUsuario { get; set; }
        public string nomeUsuario { get; set; }
        public string emailUsuario { get; set; }
        public string senhaUsuario { get; set; }


        public clsUsuarios()
        {}
        public clsUsuarios(string nome, string email,string senha)
        {
            this.nomeUsuario=nome;
            this.emailUsuario=email;
            this.senhaUsuario=senha;
        }


        public bool cadastraUsuario()
        {            
            return SQLServer.ExecutarComando("insert into Usuarios(nomeUsuario,emailUsuario,senhaUsuario) values ('"+nomeUsuario+"','"+emailUsuario+"','"+senhaUsuario+"');");
        }
        public clsUsuarios carregaUsuario(string nomeouEmailUsuario, string senhaUsuario)
        {
            this.idUsuario=0;
            this.senhaUsuario = null;
            this.nomeUsuario = null;
            this.emailUsuario = null;
            try
            {
                SqlDataReader dr = SQLServer.ExecutarConsulta("select idUsuario, nomeUsuario, emailUsuario, senhaUsuario from dbo.Usuarios where nomeUsuario='" + nomeouEmailUsuario + "' or emailUsuario='" + nomeouEmailUsuario + "' and senhaUsuario='" + senhaUsuario + "';");
                if(dr.Read())
                {
                    this.nomeUsuario = dr.GetString(1);
                    this.emailUsuario = dr.GetString(2);
                    this.senhaUsuario = dr.GetString(3);
                    this.idUsuario = dr.GetInt32(0);  //ordem de colunas da consulta
                }
            }
            catch (Exception)
            { }
            finally
            {
                SQLServer.conn.Close();
            }
            return this;
        }
    }
}