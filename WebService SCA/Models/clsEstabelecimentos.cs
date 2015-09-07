using System;
using System.Collections.Generic;

//using MySql.Data.MySqlClient;
//using Connection.MySQL;
using Connection.SQLServer;
using System.Data.SqlClient;


namespace WebService_SCA.Models
{
    public class clsEstabelecimentos
    {
        public int idCategoria {get;set;}
        public int idEstabelecimento {get;set;}
        public string nomeEstabelecimento {get;set;}
        public string enderecoEstabelecimento {get;set;}
        public string bairroEstabelecimento { get;set;}
        public string cidadeEstabelecimento {get; set;}
        public string estadoEstabelecimento { get;set;}
        public bool possuiBanheiro {get; set;}
        public float estrelasEstabelecimento { get; set;}
        public bool possuiEstacionamento{get; set;}
        public bool alturaCerta {get; set;}
        public bool possuiRampa{get; set;}
        public bool larguraSuficiente {get; set;}
        public string telefoneEstabelecimento{get; set;}
        public double latitudeEstabelecimento { get; set;}
        public double longitudeEstabelecimento { get; set;}

        //private static MySQL executor = new MySQL("b20f5b2179e89c", "96f6adfc", "br-cdbr-azure-south-a.cloudapp.net", "hefestobd");
        
        private static SQLServer executor = new SQLServer("hefestodbaws", "sanguedeunicornio", "hefestodb.ca93jazpypuz.sa-east-1.rds.amazonaws.com", "1433", "hefestodatabase", "false", "false", "30", "tcp");

        private static string sqltodos = "select idEstabelecimento,categorias_idCategoria,nomeEstabelecimento,enderecoEstabelecimento,bairroEstabelecimento,cidadeEstabelecimento,estadoEstabelecimento,mediaAvaliacao *1.0,possuiBanheiro,possuiEstacionamento,alturaCerta,possuiRampa,larguraSuficiente,telefoneEstabelecimento, latitudeEstabelecimento,longitudeEstabelecimento from view_estabelecimentos_com_avaliacao";
        public clsEstabelecimentos()
        { }
        public clsEstabelecimentos(int idEstabelecimento)
        {
            this.idEstabelecimento = idEstabelecimento;
        }
        public clsEstabelecimentos(int idCategoria, string nomeEstabelecimento, string enderecoEstabelecimento, string bairro, string cidadeEstabelecimento, string estado, float estrelasEstabelecimento, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente, string telefoneEstabelecimento, double latitudeEstabelecimento, double longitudeEstabelecimento)
        {
            this.idCategoria =idCategoria;
            this.nomeEstabelecimento= nomeEstabelecimento;
            this.enderecoEstabelecimento=enderecoEstabelecimento;
            this.bairroEstabelecimento = bairro;
            this.cidadeEstabelecimento =cidadeEstabelecimento;
            this.estadoEstabelecimento = estado;
            this.possuiBanheiro= possuiBanheiro;
            this.possuiEstacionamento=possuiEstacionamento;
            this.alturaCerta=alturaCerta;
            this.possuiRampa=possuiRampa;
            this.larguraSuficiente=larguraSuficiente;
            this.telefoneEstabelecimento=telefoneEstabelecimento;
            this.latitudeEstabelecimento=latitudeEstabelecimento;
            this.longitudeEstabelecimento = longitudeEstabelecimento;
        }
        public clsEstabelecimentos(int idEstabelecimento,int idCategoria,string nomeEstabelecimento  ,string enderecoEstabelecimento, string bairro, string cidadeEstabelecimento, string estado, float estrelasEstabelecimento, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente, string telefoneEstabelecimento, double latitudeEstabelecimento, double longitudeEstabelecimento)
        {
            this.idCategoria = idCategoria;
            this.idEstabelecimento = idEstabelecimento;
            this.nomeEstabelecimento = nomeEstabelecimento;
            this.enderecoEstabelecimento = enderecoEstabelecimento;
            this.bairroEstabelecimento = bairro;
            this.cidadeEstabelecimento = cidadeEstabelecimento;
            this.estadoEstabelecimento = estado;
            this.estrelasEstabelecimento = estrelasEstabelecimento;
            this.possuiBanheiro = possuiBanheiro;
            this.possuiEstacionamento = possuiEstacionamento;
            this.alturaCerta = alturaCerta;
            this.possuiRampa = possuiRampa;
            this.larguraSuficiente = larguraSuficiente;
            this.telefoneEstabelecimento = telefoneEstabelecimento;
            this.latitudeEstabelecimento = latitudeEstabelecimento;
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
          //  double latMax, latMin, lonMax, lonMin;
            List<clsEstabelecimentos> retorno = new List<clsEstabelecimentos>();

            /*    latMin = latitude - (raioLongoKM / 100);   
                latMax = latitude + (raioLongoKM / 100);
                lonMin = longitude - (raioLongoKM / 100);
                lonMax = longitude + (raioLongoKM / 100);*/
                
            //    sql + "where categorias_idCategoria =" + idCategoria + " and (latitudeEstabelecimento between " + latMin.ToString().Replace(",", ".") + " and " + latMax.ToString().Replace(",", ".") + ") and (longitudeEstabelecimento between " + lonMin.ToString().Replace(",", ".") + " and " + lonMax.ToString().Replace(",", ".") + ")ORDER BY (dbo.fun_CalcDistancia("+latitude+", "+longitude+", latitudeEstabelecimento, longitudeEstabelecimento));";

            SqlDataReader dr = executor.ExecutarConsulta(sqltodos + " WHERE (dbo.fun_CalcDistancia(" + latitude.ToString().Replace(",", ".") + ", " + longitude.ToString().Replace(",", ".") + ", latitudeEstabelecimento, longitudeEstabelecimento)) < " + raioLongoKM + " ORDER BY (dbo.fun_CalcDistancia(" + latitude.ToString().Replace(",", ".") + ", " + longitude.ToString().Replace(",", ".") + ", latitudeEstabelecimento, longitudeEstabelecimento)); ");
            while (dr.Read())
            {           
                retorno.Add(new clsEstabelecimentos(
                    dr.GetInt32(0),//idestabelecimento
                    dr.GetInt32(1),//idcategoria
                    dr.GetString(2),//nome
                    dr.GetString(3),//endereco
                    dr.GetString(4),//bairro
                    dr.GetString(5),//cidade
                    dr.GetString(6),//estado
                    (float)dr.GetDouble(7),//estrelas
                    dr.GetBoolean(8),//banheiro
                    dr.GetBoolean(9),//estacionamento
                    dr.GetBoolean(10),//altura
                    dr.GetBoolean(11),//rampa
                    dr.GetBoolean(12),//largura
                    dr.GetString(13),//telefone
                    Convert.ToDouble(dr.GetDecimal(14)),//latitude
                    Convert.ToDouble(dr.GetDecimal(15)))//longitude
                    );
            }
            return retorno;
        }
        public clsEstabelecimentos detalhesEstabelecimento()
        {
            SqlDataReader dr = executor.ExecutarConsulta(sqltodos + " where idEstabelecimento=" + this.idEstabelecimento + ";");
            if (dr.Read())
            {
				this.idEstabelecimento=dr.GetInt32(0);                  //idestabelecimento
				this.idCategoria=dr.GetInt32(1);                        //idcategoria
				this.nomeEstabelecimento=dr.GetString(2);               //nome
				this.enderecoEstabelecimento=dr.GetString(3);           //endereco
				this.bairroEstabelecimento=dr.GetString(4);             //bairro
				this.cidadeEstabelecimento=dr.GetString(5);             //cidade
				this.estadoEstabelecimento=dr.GetString(6);             //estado
				this.estrelasEstabelecimento=(float)dr.GetDouble(7);    //estrelas
				this.possuiBanheiro=dr.GetBoolean(8);                   //banheiro
				this.possuiEstacionamento=dr.GetBoolean(9);             //estacionamento
				this.alturaCerta=dr.GetBoolean(10);                     //altura
				this.possuiRampa= dr.GetBoolean(11);                    //rampa
				this.larguraSuficiente= dr.GetBoolean(12);              //largura
				this.telefoneEstabelecimento = dr.GetString(13);        //telefone
				this.latitudeEstabelecimento = Convert.ToDouble(dr.GetDecimal(14));//latitude
				this.longitudeEstabelecimento = Convert.ToDouble(dr.GetDecimal(15));//longitude
            }
            return this;
        }
        public static bool estabelecimentoFoiAvaliado(int idEstabelecimento,int idUsuario)
        {
            SqlDataReader dr = executor.ExecutarConsulta("select Estabelecimentos_idEstabelecimento from Usuarios_avalia_Estabelecimentos where Estabelecimentos_idEstabelecimento="+idEstabelecimento+" and Usuarios_idUsuario="+idUsuario+";");
            return dr.HasRows;
        }
        public static bool avaliaEstabelecimento(int idEstabelecimento,int idUsuario, int nota)
        {
            //se já foi avaliado, apenas atualiza a nota
             return executor.ExecutarComando(
                 "if NOT exists (select Estabelecimentos_idEstabelecimento from Usuarios_avalia_Estabelecimentos where Estabelecimentos_idEstabelecimento="+idEstabelecimento+" and Usuarios_idUsuario="+idUsuario+")"+
                        " begin "+
                            "INSERT INTO Usuarios_avalia_Estabelecimentos (Estabelecimentos_idEstabelecimento, Usuarios_idUsuario, notaAvaliacaoIndividual) VALUES(" + idEstabelecimento + "," + idUsuario + ","+nota+");" +
                        " end"+
                    " else "+
		                "begin "+
                            "UPDATE Usuarios_avalia_Estabelecimentos SET notaAvaliacaoIndividual = " + nota + " WHERE Estabelecimentos_idEstabelecimento= " + idEstabelecimento + " AND Usuarios_idUsuario = " + idUsuario + "" +
		                " end"
            );
        }
        /// <summary>
        /// cadastra o estabelecimento passado e retorna o seu id no banco de dados
        /// </summary>
        /// <returns>inteiro com o id do estabelecimento cadastrado</returns>
        public int cadastraEstabelecimento()
        {
            SqlDataReader dr = executor.ExecutarConsulta("insert Estabelecimentos(categorias_idCategoria,nomeEstabelecimento,enderecoEstabelecimento,bairroEstabelecimento,cidadeEstabelecimento,estadoEstabelecimento,possuiBanheiro,possuiEstacionamento,alturaCerta,possuiRampa,larguraSuficiente,telefoneEstabelecimento,latitudeEstabelecimento,longitudeEstabelecimento) "+
            "values(" + this.idCategoria + ",'" + this.nomeEstabelecimento + "','" + this.enderecoEstabelecimento + "','" + this.bairroEstabelecimento + "','" + this.cidadeEstabelecimento + "','" + this.estadoEstabelecimento + "'," + Convert.ToInt16(this.possuiBanheiro) + "," + Convert.ToInt16(this.possuiEstacionamento) + "," + Convert.ToInt16(this.alturaCerta) + "," + Convert.ToInt16(this.possuiRampa) + "," + Convert.ToInt16(this.larguraSuficiente) + ",'" + this.telefoneEstabelecimento + "'," + this.latitudeEstabelecimento.ToString().Replace(",", ".") + "," + this.longitudeEstabelecimento.ToString().Replace(",", ".") + "); " +
                //"SELECT SCOPE_IDENTITY();");                                                                                                                                                                                                                                                                                                                                                                                                                                                
                "select last_insert_id();"); 
            dr.Read();
            return Convert.ToInt32(dr.GetDecimal(0));
        }
        public bool editaEstabelecimento()
        {
            return executor.ExecutarComando("UPDATE Estabelecimentos SET " +
            "categorias_idCategoria=" + this.idCategoria + ",nomeEstabelecimento='" + this.nomeEstabelecimento + "',enderecoEstabelecimento='" + this.enderecoEstabelecimento + "',bairroEstabelecimento='" + this.bairroEstabelecimento + "',cidadeEstabelecimento='" + this.cidadeEstabelecimento + "',estadoEstabelecimento='" + this.estadoEstabelecimento + "',possuiBanheiro=" + Convert.ToInt16(this.possuiBanheiro) + ",possuiEstacionamento=" + Convert.ToInt16(this.possuiEstacionamento) + ",alturaCerta=" + Convert.ToInt16(this.alturaCerta) + ",possuiRampa=" + Convert.ToInt16(this.possuiRampa) + ",larguraSuficiente=" + Convert.ToInt16(this.larguraSuficiente) + ",telefoneEstabelecimento='" + this.telefoneEstabelecimento + "',latitudeEstabelecimento=" + this.latitudeEstabelecimento.ToString().Replace(",", ".") + ",longitudeEstabelecimento=" + this.longitudeEstabelecimento.ToString().Replace(",", ".") + " where idEstabelecimento=" + this.idEstabelecimento);
        }
    }
}