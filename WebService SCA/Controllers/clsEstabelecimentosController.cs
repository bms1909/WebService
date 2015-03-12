using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_SCA.Models;
using Newtonsoft.Json;

namespace WebService_SCA.Controllers
{

    public class clsEstabelecimentosController : ApiController
    {
        // GET api/clsestabelecimentos/5
        
        public List<clsEstabelecimentos> Get(float raioLongoKM,double latitude,double longitude)
        {
            return clsEstabelecimentos.estabelecimentosPorRaio(raioLongoKM,latitude,longitude);
        }
        public List<clsEstabelecimentos> Get(int idCategoria)
        {
            return clsEstabelecimentos.estabelecimentosPorCategoria(idCategoria);
        }
        public clsEstabelecimentos Get(int SemUso,int idEstabelecimento)//diferenciar as assinaturas do método padrão GET
        {
           clsEstabelecimentos detalha = new clsEstabelecimentos(idEstabelecimento);
           return detalha.detalhesEstabelecimento(); 
        }

        // POST api/clsestabelecimentos
        public bool Post(int idCategoria, string nomeEstabelecimento, string enderecoEstabelecimento, string cidadeEstabelecimento, int estrelasAtendimento, int avaliadoresEstrelas, bool possuiBanheiro, bool possuiEstacionamento, bool alturaCerta, bool possuiRampa, bool larguraSuficiente, string telefoneEstabelecimento, double latitudeEstabelecimento, double longitudeEstabelecimento)
        {
            clsEstabelecimentos cadastra = new clsEstabelecimentos( idCategoria, nomeEstabelecimento, enderecoEstabelecimento, cidadeEstabelecimento,  estrelasAtendimento, avaliadoresEstrelas, possuiBanheiro, possuiEstacionamento, alturaCerta, possuiRampa,larguraSuficiente,telefoneEstabelecimento,latitudeEstabelecimento,longitudeEstabelecimento );
            return cadastra.cadastraEstabelecimento();
        }

        // PUT api/clsestabelecimentos/5
        public void Post(int id,int nota)
        {
            clsEstabelecimentos.avaliaEstabelecimento(id,nota);
        }

    }
}
