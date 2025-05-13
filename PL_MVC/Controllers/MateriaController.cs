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
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            if (IdMateria > 0)
            {
                ML.Result result = BL.Materia.GetById(IdMateria.Value);
                materia = (ML.Materia)result.Object;
            }

            materia.Semestre = new ML.Semestre();
            materia.Semestre.Grupo = new ML.Grupo();

            ML.Result resultSemestre = BL.Materia.SemestreGetAll();

            if (resultSemestre.Correct)
            {
                materia.Semestre.Semestres = resultSemestre.Objects;
            }

            return View(materia);
        }


        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {

            if (materia.IdMateria > 0)
            {
                ML.Result result = BL.Materia.Update(materia);
            }
            else
            {
                materia.Semestre = new ML.Semestre();

                materia.Semestre.IdSemestre = 19;
                BL.Materia.AddEFSP(materia);

                // Redireccionar al GetAll

                //return View("GetAll");
            }


            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {
            BL.Materia.DeleteEFSP(IdMateria);

            return RedirectToAction("GetAll");
        }

        public JsonResult GetGrupoByIdSemestre(int IdSemestre)
        {
            ML.Result resultGrupos = BL.Grupo.GetByIdSemestre(IdSemestre);

            return Json(resultGrupos, JsonRequestBehavior.AllowGet);
        }

    }
}