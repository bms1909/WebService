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
        public bool Get(string nomeOuEmail)
        {
            return clsUsuarios.recuperaUsuario(nomeOuEmail);
        }
        public clsUsuarios Get(string nomeouEmail, string senha)
        {
            return clsUsuarios.carregaUsuario(nomeouEmail,nomeouEmail,senha);
        }

        // POST api/clsusuarios
        public String Post(string nome, string email, string senha)
        {
            clsUsuarios grava = new clsUsuarios(0,nome,email,senha);
            return grava.cadastraUsuario();
        }

    }
}
