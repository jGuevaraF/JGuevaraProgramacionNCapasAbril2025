using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("digis")]
    public class CalculadoraController : ApiController
    {
        //
        [HttpGet]
        [Route("SumaDeDosNumerosEnteros/{numero1}/{numero2}")]
        public IHttpActionResult Suma(int numero1, int numero2)
        {
            //resultado de la suma
            //Codigo estado
            Guid id = new Guid();
            
            return Content(HttpStatusCode.OK, numero1 + numero2);
        }

        [HttpGet]
        [Route("Resta/{numero1}/{numero2}")]
        public IHttpActionResult Resta(int numero1, int numero2)
        {
            //resultado de la suma
            //Codigo estado
            return Content(HttpStatusCode.OK, numero1 + numero2);
        }
    }
}
