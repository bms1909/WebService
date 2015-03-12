using Connection.SQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
            return SQLServer.ExecutarComando("INSERT Alertas(Usuarios_idUsuario,latitudeAlerta,longitudeAlerta,tipoAlerta,descricaoAlerta,riscoAlerta) values(" + this.idUsuario + "," + this.latitudeAlerta.ToString().Replace(",", ".") + "," + this.longitudeAlerta.ToString().Replace(",", ".") + "," + this.tipoAlerta + ",'" + this.descricaoAlerta + "'," + this.riscoAlerta + ");");
        }
        public static bool denunciaAlerta(int idAlerta)
        {
            return SQLServer.ExecutarComando("update Alertas set denunciasAlerta =denunciasAlerta+1 where idAlerta="+idAlerta+";");
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
            raioLongoKM = raioLongoKM * 2;
            List<clsAlertas> retorno = new List<clsAlertas>();
            if(raioLongoKM<11)
            {
                latMin = latitude - (raioLongoKM/100);   //0.0X
                latMax = latitude + (raioLongoKM/100);
                lonMin = longitude - (raioLongoKM / 100);
                lonMax = longitude + (raioLongoKM / 100);
            }
            else if(raioLongoKM<111)
            {
                latMin = latitude - (raioLongoKM / 10);   //0.X
                latMax = latitude + (raioLongoKM / 10);
                lonMin = longitude - (raioLongoKM / 10);
                lonMax = longitude + (raioLongoKM / 10);
            }
            else
            {
                latMin = latitude - raioLongoKM ;   //X.0
                latMax = latitude + raioLongoKM ;
                lonMin = longitude - raioLongoKM;
                lonMax = longitude + raioLongoKM;
            }
            SqlDataReader dr = SQLServer.ExecutarConsulta("select Usuarios_idUsuario,idAlerta,latitudeAlerta,longitudeAlerta,tipoAlerta,descricaoAlerta,riscoAlerta from Alertas where  (latitudeAlerta between " + latMin.ToString().Replace(",", ".") + " and " + latMax.ToString().Replace(",", ".") + ") and (longitudeAlerta between " + lonMin.ToString().Replace(",", ".") + " and " + lonMax.ToString().Replace(",", ".") + ");");
            while(dr.Read())
            {
                retorno.Add(new clsAlertas(dr.GetInt32(0),dr.GetInt32(1),Convert.ToDouble(dr.GetDecimal(2)),Convert.ToDouble(dr.GetDecimal(3)),dr.GetInt32(4),dr.GetString(5),dr.GetInt32(6)));
            }
            return retorno;
        }
    }
}