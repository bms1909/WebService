using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_SCA.Models;

namespace WebService_SCA.Controllers
{
    public class clsEstabelecimentosController : ApiController
    {
        // GET api/clsestabelecimentos/
        public List<clsEstabelecimentos> Get(float raioLongoKM,double latitude,double longitude)
        {
            return clsEstabelecimentos.estabelecimentosPorRaio(raioLongoKM,latitude,longitude);
        }

        public clsEstabelecimentos Get(int idEstabelecimento)
        {
           clsEstabelecimentos detalha = new clsEstabelecimentos(idEstabelecimento);
           return detalha.detalhesEstabelecimento(); 
        }
        public bool Get(int idUsuario,int idEstabelecimento)
        {
            return clsEstabelecimentos.estabelecimentoFoiAvaliado(idEstabelecimento,idUsuario);
        }     

        // POST api/clsestabelecimentos
        public bool Post(int idCategoria, string nomeEstabelecimento, string enderecoEstabelecimento, string bairro, string cidadeEstabelecimento, string estado, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente, string telefoneEstabelecimento, double latitudeEstabelecimento, double longitudeEstabelecimento,int idUsuario,int nota)
        {
            clsEstabelecimentos cadastra = new clsEstabelecimentos(idCategoria, nomeEstabelecimento, enderecoEstabelecimento, bairro, cidadeEstabelecimento, estado, 0.0f, possuiBanheiro, possuiEstacionamento, alturaCerta, possuiRampa, larguraSuficiente, telefoneEstabelecimento, latitudeEstabelecimento, longitudeEstabelecimento);
            int id = cadastra.cadastraEstabelecimento();
            if (id > 0)
            {
                return clsEstabelecimentos.avaliaEstabelecimento(id, idUsuario, nota);
            }
                return false;
        }

        // POST api/clsestabelecimentos
        public bool Post(int idEstabelecimento,int idUsuario,int nota)
        {
            return clsEstabelecimentos.avaliaEstabelecimento(idEstabelecimento,idUsuario,nota);
        }
        public bool Post(int idEstabelecimento, int idCategoria, string nomeEstabelecimento, string enderecoEstabelecimento, string bairro, string cidadeEstabelecimento, string estado, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente, string telefoneEstabelecimento, double latitudeEstabelecimento, double longitudeEstabelecimento, int idUsuario, int nota)
        {
           clsEstabelecimentos edita = new clsEstabelecimentos(idEstabelecimento,idCategoria, nomeEstabelecimento, enderecoEstabelecimento, bairro, cidadeEstabelecimento, estado, 0.0f, possuiBanheiro, possuiEstacionamento, alturaCerta, possuiRampa, larguraSuficiente, telefoneEstabelecimento, latitudeEstabelecimento, longitudeEstabelecimento);
           if (edita.editaEstabelecimento())
           {
               return clsEstabelecimentos.avaliaEstabelecimento(idEstabelecimento, idUsuario, nota);
           }
           return false;
        }
       

    }
}
