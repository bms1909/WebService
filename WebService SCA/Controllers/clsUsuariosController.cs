using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_SCA.Models;

namespace WebService_SCA.Controllers
{
    public class clsUsuariosController : ApiController
    {

        // GET api/clsusuarios/5
        public clsUsuarios Get(string nomeouEmail, string senha)
        {
            clsUsuarios consulta = new clsUsuarios();
            return consulta.carregaUsuario(nomeouEmail,senha);
        }

        // POST api/clsusuarios
        public bool Post(string nome, string email, string senha)
        {
            clsUsuarios grava = new clsUsuarios(nome,email,senha);
            return grava.cadastraUsuario();
        }

    }
}
