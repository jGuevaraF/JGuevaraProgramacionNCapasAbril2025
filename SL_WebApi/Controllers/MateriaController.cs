using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/Materia")]
    public class MateriaController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Nombre = "";
            materia.Semestre = new ML.Semestre();
            materia.Semestre.IdSemestre = 0;

            ML.Result result = BL.Materia.GetAllSP(materia);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            } else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("BusquedaAbierta")]
        public IHttpActionResult BusquedaAbierta([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.GetAllSP(materia);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }


        [HttpDelete]
        [Route("Delete/{IdMateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Result result = BL.Materia.DeleteSP(IdMateria);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }


        [HttpPut]
        [Route("Update/{IdMateria}")]
        public IHttpActionResult Update(int IdMateria, [FromBody] ML.Materia materia)
        {
            materia.IdMateria = IdMateria;

            ML.Result result = BL.Materia.Update(materia);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }



    }
}
