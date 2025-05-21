using Microsoft.Ajax.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Web;
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
            materia.Semestre = new ML.Semestre();
            ML.Result result = new ML.Result();

            //TODOS

            materia.Nombre = "";
            materia.Semestre.IdSemestre = 0;

            result = BL.Materia.GetAllSP(materia);

            if (result.Correct)
            {
                materia.Materias = result.Objects;
                ML.Result resultSemestres = BL.Materia.SemestreGetAll();
                if (resultSemestres.Correct)
                {
                    materia.Semestre.Semestres = resultSemestres.Objects;
                }

            }
            return View(materia);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Materia Materia)
        {


            //Materia.Nombre = Materia.Nombre == null ? "" : Materia.Nombre;
            Materia.Nombre = Materia.Nombre ?? "";
            ML.Result result = BL.Materia.GetAllSP(Materia);

            ML.Result resultSemestres = BL.Materia.SemestreGetAll();
            if (resultSemestres.Correct)
            {
                Materia.Semestre.Semestres = resultSemestres.Objects;
            }


            if (result.Correct)
            {
                Materia.Materias = result.Objects;
            }


            Materia.Nombre = "";
            Materia.Semestre.IdSemestre = 0;
            //apellidos
            //Rol.IdRol = 0
            return View(Materia);
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();


            if (IdMateria > 0)
            {
                ML.Result result = BL.Materia.GetByIdEFSP(IdMateria.Value);
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
            // arreglo de bytes => Byte []

            HttpPostedFileBase inptImgagenMateria = Request.Files["inptImgagenMateria"];


            if (inptImgagenMateria.ContentLength > 0)
            {
                using (Stream inputStream = inptImgagenMateria.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    materia.Imagen = memoryStream.ToArray();
                }
            }




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


        public JsonResult MateriaGetByIdSemestre(int IdSemestre)
        {
            ML.Result resultMateria = BL.Materia.MAteriGetByIdSemestre(IdSemestre);

            return Json(resultMateria, JsonRequestBehavior.AllowGet);
        }



    }
}