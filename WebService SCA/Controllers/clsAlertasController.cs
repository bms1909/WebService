using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_SCA.Models;

namespace WebService_SCA.Controllers
{
    public class clsAlertasController : ApiController
    {
        // GET api/clsalertas
        public List<clsAlertas> Get(float raioLongoemKM,double lat,double lon)
        {
            return clsAlertas.carregaAlertas(raioLongoemKM,lat,lon);
        }

        // POST api/clsalertas
        public bool Post(int idUsuario,double lat,double lon,int tipo,string descricao, int risco)
        {
            clsAlertas cadastra = new clsAlertas(idUsuario, lat, lon, tipo, descricao, risco);
            return cadastra.cadastraAlerta();
        }
        public bool Post(int idAlerta, int tipo,string descricao, int risco)
        {
            return clsAlertas.editaAlerta(idAlerta,tipo,descricao,risco);
        }
        public bool Post(int idAlerta)
        {
            return clsAlertas.excluiAlerta(idAlerta);
        }

        public bool Post(int idAlerta, int idUsuario)
        {
           return clsAlertas.denunciaAlerta(idAlerta, idUsuario);
        }
    }
}
