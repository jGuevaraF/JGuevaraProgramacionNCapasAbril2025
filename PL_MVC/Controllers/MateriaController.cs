using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = new ML.Result();

            result = BL.Materia.GetAllSP();

            if (result.Correct)
            {
                materia.Materias = result.Objects;
            }

            return View(materia);
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.Materia materia = new ML.Materia();
            return View(materia);
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia) { 
        

            materia.Semestre = new ML.Semestre();

            materia.Semestre.IdSemestre = 19;
            BL.Materia.AddEFSP(materia);

            // Redireccionar al GetAll

            //return View("GetAll");
            return RedirectToAction("GetAll");
        }

        [HttpDelete]
        public ActionResult Delete(int IdMateria)
        {
            BL.Materia.DeleteEFSP(IdMateria);

            return RedirectToAction("GetAll");
        }

    }
}