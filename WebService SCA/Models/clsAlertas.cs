
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using MySql.Data.MySqlClient;
//using Connection.MySQL;
using Connection.SQLServer;
using System.Data.SqlClient;

namespace WebService_SCA.Models
{
    public class clsAlertas
    {
        public int idAlerta{get;set;}
        public int idUsuario {get; set;}
        public double latitudeAlerta{get; set;}
        public double longitudeAlerta{get; set;}
        public int tipoAlerta{get; set;}
        public string descricaoAlerta{get; set;}
        public int riscoAlerta {get; set;}

        //private static MySQL executor = new MySQL("b20f5b2179e89c", "96f6adfc", "br-cdbr-azure-south-a.cloudapp.net", "hefestobd");
        private static SQLServer executor = new SQLServer("hefestodbaws", "sanguedeunicornio", "hefestodb.ca93jazpypuz.sa-east-1.rds.amazonaws.com", "1433", "hefestodatabase", "false", "false", "30", "tcp");

        public clsAlertas()
        { }
        public clsAlertas(int Usuarios_idUsuario, double latitudeAlerta, double longitudeAlerta, int tipoAlerta, string descricaoAlerta, int riscoAlerta)
        {
            this.idUsuario = Usuarios_idUsuario;
            this.latitudeAlerta = latitudeAlerta;
            this.longitudeAlerta = longitudeAlerta;
            this.tipoAlerta = tipoAlerta;
            this.descricaoAlerta = descricaoAlerta;
            this.riscoAlerta = riscoAlerta;
        }
        public clsAlertas(int Usuarios_idUsuario,int idAlerta,double latitudeAlerta,double longitudeAlerta,int tipoAlerta,string descricaoAlerta, int riscoAlerta)
        {
            this.idUsuario= Usuarios_idUsuario;
            this.idAlerta= idAlerta;
            this.latitudeAlerta= latitudeAlerta;
            this.longitudeAlerta=longitudeAlerta;
            this.tipoAlerta= tipoAlerta;
            this.descricaoAlerta= descricaoAlerta;
            this.riscoAlerta = riscoAlerta;
        }
    
        public bool cadastraAlerta()
        {
            return executor.ExecutarComando(
            "INSERT Alertas(Usuarios_idUsuario,latitudeAlerta,longitudeAlerta,tipoAlerta,descricaoAlerta,riscoAlerta)"+ 
            "values(" + this.idUsuario + "," + this.latitudeAlerta.ToString().Replace(",", ".") + "," + this.longitudeAlerta.ToString().Replace(",", ".") + "," + this.tipoAlerta + ",'" + this.descricaoAlerta + "'," + this.riscoAlerta + ");");
        }
        public static bool denunciaAlerta(int idAlerta,int idUsuario)
        {
            //se denuncia já existe, ignora, caso contrário, insere
            return executor.ExecutarComando("if NOT exists (select alertas_idalerta from usuarios_denunciam_alertas where Alertas_idAlerta=" + idAlerta + " and Usuarios_idUsuario=" + idUsuario + ")" +
            " begin "+
                "insert into usuarios_denunciam_alertas(Alertas_idAlerta,Usuarios_idUsuario) values ("+idAlerta+","+idUsuario+")"+
            " end");
        }
        public static List<clsAlertas> carregaAlertas(float raioLongoKM,double latitude,double longitude)
        {
            /*é possível fazer uma correspondência aproximada de coordenadas geográficas com os seguintes valores:
             * EX: -31.245678 
             * 1=possui precisão de 111.11km
             * 2=precisão de 11.1km
             * 4=1.11km
             * 5=110m
             * 6=11m
             * 7=1.1m
             * 8=0.11m
             */
            double latMax,latMin,lonMax,lonMin;
            List<clsAlertas> retorno = new List<clsAlertas>();

            latMin = latitude - (raioLongoKM / 100);
            latMax = latitude + (raioLongoKM / 100);
            lonMin = longitude - (raioLongoKM / 100);
            lonMax = longitude + (raioLongoKM / 100);

            SqlDataReader dr = executor.ExecutarConsulta("select Usuarios_idUsuario,idAlerta,latitudeAlerta,longitudeAlerta,tipoAlerta,descricaoAlerta,riscoAlerta from Alertas where  (latitudeAlerta between " + latMin.ToString().Replace(",", ".") + " and " + latMax.ToString().Replace(",", ".") + ") and (longitudeAlerta between " + lonMin.ToString().Replace(",", ".") + " and " + lonMax.ToString().Replace(",", ".") + ");");
            while(dr.Read())
            {
                retorno.Add(new clsAlertas(dr.GetInt32(0),dr.GetInt32(1),Convert.ToDouble(dr.GetDecimal(2)),Convert.ToDouble(dr.GetDecimal(3)),dr.GetInt32(4),dr.GetString(5),dr.GetInt32(6)));
            }
            return retorno;
        }

        public static bool editaAlerta(int idAlerta, int tipo, string descricao, int risco)
        {
            return executor.ExecutarComando("UPDATE Alertas set tipoAlerta=" + tipo + " ,descricaoAlerta= '" + descricao + "' ,riscoAlerta=" + risco + " where idAlerta= " + idAlerta + ";");
        }

        public static bool excluiAlerta(int idAlerta)
        {
            return executor.ExecutarComando("DELETE from Alertas where idAlerta=" + idAlerta + ";");
        }
    }
}