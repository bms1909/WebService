using Connection.SQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebService_SCA.Models
{
    public class clsEstabelecimentos
    {
        public int idCategoria {get; set;}
        public int idEstabelecimento {get; set;}
        public string nomeEstabelecimento {get; set;}
        public string enderecoEstabelecimento {get; set;}
        public string cidadeEstabelecimento {get; set;}
        public int estrelasAtendimento{get; set;}
        public int avaliadoresEstrelas{get; set;}
        public bool possuiBanheiro{get; set;}
        public bool possuiEstacionamento{get; set;}
        public bool alturaCerta {get; set;}
        public bool possuiRampa{get; set;}
        public bool larguraSuficiente {get; set;}
        public string telefoneEstabelecimento{get; set;}
        public double latitudeEstabelecimento { get; set; }
        public double longitudeEstabelecimento { get; set; }

        public clsEstabelecimentos()
        { }
        public clsEstabelecimentos(int idEstabelecimento)
        {
            this.idEstabelecimento = idEstabelecimento;
        }
        public clsEstabelecimentos(int idCategoria, string nome, int estrela, int aval,double lat, double lon)
        {
            this.idCategoria = idCategoria;
            this.nomeEstabelecimento = nome;
            this.estrelasAtendimento = estrela;
            this.avaliadoresEstrelas = aval;
            this.latitudeEstabelecimento = lat;
            this.longitudeEstabelecimento = lon;
        }
        public clsEstabelecimentos(int idCategoria, string nomeEstabelecimento, string enderecoEstabelecimento, string cidadeEstabelecimento, int estrelasAtendimento, int avaliadoresEstrelas, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente,string telefoneEstabelecimento, double latitudeEstabelecimento,double longitudeEstabelecimento )
        {
            this.idCategoria =idCategoria;
            this.nomeEstabelecimento= nomeEstabelecimento;
            this.enderecoEstabelecimento=enderecoEstabelecimento;
            this.cidadeEstabelecimento =cidadeEstabelecimento;
            this.estrelasAtendimento=estrelasAtendimento;
            this.avaliadoresEstrelas =avaliadoresEstrelas;
            this.possuiBanheiro= possuiBanheiro;
            this.possuiEstacionamento=possuiEstacionamento;
            this.alturaCerta=alturaCerta;
            this.possuiRampa=possuiRampa;
            this.larguraSuficiente=larguraSuficiente;
            this.telefoneEstabelecimento=telefoneEstabelecimento;
            this.latitudeEstabelecimento=latitudeEstabelecimento;
            this.longitudeEstabelecimento = longitudeEstabelecimento;
        }

        public static List<clsEstabelecimentos> estabelecimentosPorRaio(float raioLongoKM, double latitude, double longitude)
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
            double latMax, latMin, lonMax, lonMin;
            raioLongoKM = raioLongoKM * 2;
            List<clsEstabelecimentos> retorno = new List<clsEstabelecimentos>();

            if (raioLongoKM < 11)
            {
                latMin = latitude - (raioLongoKM / 100);   //0.0X
                latMax = latitude + (raioLongoKM / 100);
                lonMin = longitude - (raioLongoKM / 100);
                lonMax = longitude + (raioLongoKM / 100);
            }
            else if (raioLongoKM < 111)
            {
                latMin = latitude - (raioLongoKM / 10);   //0.X
                latMax = latitude + (raioLongoKM / 10);
                lonMin = longitude - (raioLongoKM / 10);
                lonMax = longitude + (raioLongoKM / 10);
            }
            else
            {
                latMin = latitude - raioLongoKM;   //X.0
                latMax = latitude + raioLongoKM;
                lonMin = longitude - raioLongoKM;
                lonMax = longitude + raioLongoKM;
            }

            SqlDataReader dr = SQLServer.ExecutarConsulta("select idEstabelecimento,nomeEstabelecimento,estrelasAtendimento,avaliadoresEstrelas,latitudeEstabelecimento,longitudeEstabelecimento from Estabelecimentos where (latitudeEstabelecimento between " + latMin.ToString().Replace(",", ".") + " and " + latMax.ToString().Replace(",", ".") + ") and (longitudeEstabelecimento between " + lonMin.ToString().Replace(",", ".") + " and " + lonMax.ToString().Replace(",", ".") + ");");
            while (dr.Read())
            {
                retorno.Add(new clsEstabelecimentos(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetDouble(4), dr.GetDouble(5)));
            }
            return retorno;
        }

        public static List<clsEstabelecimentos> estabelecimentosPorCategoria(int idCategoria)
        {
            List<clsEstabelecimentos> retorno = new List<clsEstabelecimentos>();
            SqlDataReader dr = SQLServer.ExecutarConsulta("select idEstabelecimento,nomeEstabelecimento,estrelasAtendimento,avaliadoresEstrelas,latitudeEstabelecimento,longitudeEstabelecimento from Estabelecimentos where Categorias_idCategoria="+idCategoria+";");
            while (dr.Read())
            {
                retorno.Add(new clsEstabelecimentos(dr.GetInt32(0),dr.GetString(1),dr.GetInt32(2),dr.GetInt32(3),Convert.ToDouble(dr.GetDecimal(4)),Convert.ToDouble(dr.GetDecimal(5))));
            }
            return retorno;
        }
        public clsEstabelecimentos detalhesEstabelecimento()
        {
            SqlDataReader dr = SQLServer.ExecutarConsulta("select categorias_idCategoria,idEstabelecimento,nomeEstabelecimento,enderecoEstabelecimento,cidadeEstabelecimento,estrelasAtendimento,avaliadoresEstrelas,possuiBanheiro,possuiEstacionamento,alturaCerta,possuiRampa,larguraSuficiente,telefoneEstabelecimento, latitudeEstabelecimento,longitudeEstabelecimento from Estabelecimentos where idEstabelecimento="+this.idEstabelecimento+";");
            if (dr.Read())
            {
                this.idCategoria=dr.GetInt32(0);
                this.idEstabelecimento=dr.GetInt32(1);
                this.nomeEstabelecimento=dr.GetString(2);
                this.enderecoEstabelecimento=dr.GetString(3);
                this.cidadeEstabelecimento=dr.GetString(4);
                this.estrelasAtendimento=dr.GetInt32(5);
                this.avaliadoresEstrelas=dr.GetInt32(6);
                this.possuiBanheiro=dr.GetBoolean(7);
                this.possuiEstacionamento=dr.GetBoolean(8);
                this.alturaCerta=dr.GetBoolean(9);
                this.possuiRampa=dr.GetBoolean(10);
                this.larguraSuficiente=dr.GetBoolean(11);
                this.telefoneEstabelecimento = dr.GetString(12);
                this.latitudeEstabelecimento = Convert.ToDouble(dr.GetDecimal(13));
                this.longitudeEstabelecimento =Convert.ToDouble(dr.GetDecimal(14));
            }
            return this;
        }
        public static bool avaliaEstabelecimento(int id, int nota)
        {
            return SQLServer.ExecutarComando("update Estabelecimentos set estrelasAtendimento=estrelasAtendimento + "+nota+", avaliadoresEstrelas=avaliadoresEstrelas + 1 where idEstabelecimento="+id+";");
        }
        public bool cadastraEstabelecimento()
        {
            return SQLServer.ExecutarComando("insert Estabelecimentos(categorias_idCategoria,nomeEstabelecimento,enderecoEstabelecimento,cidadeEstabelecimento,estrelasAtendimento,avaliadoresEstrelas,possuiBanheiro,possuiEstacionamento,alturaCerta,possuiRampa,larguraSuficiente,telefoneEstabelecimento,latitudeEstabelecimento,longitudeEstabelecimento) values(" + this.idCategoria + ",'" + this.nomeEstabelecimento + "','" + this.enderecoEstabelecimento + "','" + this.cidadeEstabelecimento + "'," + this.estrelasAtendimento + "," + this.avaliadoresEstrelas + "," + Convert.ToInt16(this.possuiBanheiro) + "," + Convert.ToInt16(this.possuiEstacionamento) + "," + Convert.ToInt16(this.alturaCerta) + "," + Convert.ToInt16(this.possuiRampa) + "," + Convert.ToInt16(this.larguraSuficiente) + ",'" + this.telefoneEstabelecimento + "'," + this.latitudeEstabelecimento.ToString().Replace(",", ".") + "," + this.longitudeEstabelecimento.ToString().Replace(",", ".") + ");");
        }
    }
}