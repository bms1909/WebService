using SendGrid;
using System.Net.Mail;
using System.Net;


//using Connection.MySQL;
//using MySql.Data.MySqlClient;

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

        //private static MySQL executor = new MySQL("b20f5b2179e89c", "96f6adfc", "br-cdbr-azure-south-a.cloudapp.net", "hefestobd");
        private static SQLServer executor = new SQLServer("hefestodbaws", "sanguedeunicornio", "hefestodb.ca93jazpypuz.sa-east-1.rds.amazonaws.com", "1433", "hefestodatabase","false","false","30","tcp");
        public clsUsuarios()
        {}
        public clsUsuarios(int id,string nome, string email,string senha)
        {
            this.idUsuario = id;
            this.nomeUsuario=nome;
            this.emailUsuario=email;
            this.senhaUsuario=senha;
        }



        public string cadastraUsuario()
        {
            if (executor.ExecutarComando("insert into Usuarios(nomeUsuario,emailUsuario,senhaUsuario) values ('" + nomeUsuario + "','" + emailUsuario + "','" + senhaUsuario + "');"))
            {
                return "SUCESSO";
            }
            clsUsuarios temp = carregaUsuario(this.nomeUsuario,this.emailUsuario, this.senhaUsuario);
            //previne senha correta ou não ao recuperar usuário
            if (temp.idUsuario>0||temp.senhaUsuario=="INCORRETA")
            {
                return "JA_CADASTRADO";
            }
            //erro
            return "WEBSERVICE_CADASTRO_DESCONHECIDO";
        }
        public static bool recuperaUsuario(string nomeOuEmail)
        {
            clsUsuarios usuarioRecuperar = new clsUsuarios();
            SqlDataReader dr = executor.ExecutarConsulta("select nomeUsuario,emailUsuario, senhaUsuario from dbo.Usuarios where nomeUsuario='" + nomeOuEmail + "' or emailUsuario='" + nomeOuEmail + "';");
            //MySqlDataReader dr = executor.ExecutarConsulta("select nomeUsuario,emailUsuario, senhaUsuario from Usuarios where nomeUsuario='" + nomeOuEmail + "' or emailUsuario='" + nomeOuEmail + "';");
            if (dr.Read())
            {   
                usuarioRecuperar.nomeUsuario = dr.GetString(0);
                usuarioRecuperar.emailUsuario = dr.GetString(1);
                usuarioRecuperar.senhaUsuario = dr.GetString(2);

                //exemplo modificado obtido em: http://azure.microsoft.com/pt-br/documentation/articles/sendgrid-dotnet-how-to-send-email/
                // Create the email object first, then add the properties.
                SendGridMessage myMessage = new SendGridMessage();
                myMessage.AddTo(usuarioRecuperar.emailUsuario);
                myMessage.From = new MailAddress("suporte@hefesto.com", "Suporte Hefesto");
                myMessage.Subject = "Recuperação de Senha";
                myMessage.Text = "Conforme solicitado no aplicativo, segue dados de acesso ao Hefesto: \n"+
                    " Nome de usuário: "+usuarioRecuperar.nomeUsuario+"\n"+
                    " Senha: "+usuarioRecuperar.senhaUsuario+"\n"+
                    "Caso não tenha solicitado recuperação de senha, por favor, desconsidere este email";

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential("azure_485641dfb5c882d3ab5636f35ded66e9@azure.com", "uFel0Y2K10PGi9C");

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                // You can also use the **DeliverAsync** method, which returns an awaitable task.
                transportWeb.DeliverAsync(myMessage);
                return true;
            }
            return false;
        }


        public static clsUsuarios carregaUsuario(string nomeUsuarioCarga,string emailUsuarioCarga, string senhaUsuario)
        {
            int idUsuario=0;
            string nomeUsuario = "INVALIDO";
            string emailUsuario = nomeUsuario;
            SqlDataReader dr = executor.ExecutarConsulta("select idUsuario, nomeUsuario, emailUsuario, senhaUsuario from dbo.Usuarios where nomeUsuario='" + nomeUsuarioCarga + "' or emailUsuario='" + emailUsuarioCarga + "';");
            if (dr.Read())
            {
                //senha se torna case sensitive e permite personalizar o retorno
                if (dr.GetString(3) == senhaUsuario)
                {
                    nomeUsuario = dr.GetString(1);
                    emailUsuario = dr.GetString(2);
                    senhaUsuario = dr.GetString(3);
                    idUsuario = dr.GetInt32(0);  //ordem de colunas da consulta
                }
                else
                {
                    //código de erro
                    senhaUsuario = "INCORRETA";
                }
            }
            return new clsUsuarios(idUsuario,nomeUsuario,emailUsuario,senhaUsuario);
        }
    }
}