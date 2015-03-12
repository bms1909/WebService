using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_SCA.Models;

namespace WebService_SCA.Controllers
{
    public class clsCategoriasController : ApiController
    {
        // GET api/clscategorias/5
        public List<clsCategorias> Get()
        {
            return clsCategorias.carregaCategorias();
        }
    }
}
