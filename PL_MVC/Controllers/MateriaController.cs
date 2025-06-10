using BL;
using Microsoft.Ajax.Utilities;
using ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {

        //public ActionResult Descargar()
        //{
        //    // extraer de su session 
        //    string rutaErrores = Session["Errores"] as string;

        //    Session["Errores"] = null;
        //    return File();
        //}


        public ActionResult GuardarDatos()
        {

            // Extraer de tu sesion la ruta
            string ruta = "";

            using (StreamReader sr = new StreamReader(ruta))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    // Leer cada linea
                    string[] campos = sr.ReadLine().Split('|');

                    ML.Materia materia = new ML.Materia();
                    materia.Nombre = campos[0];



                    BL.Materia.Add(materia);
                }
            }

            return View();
        }

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

            //result = BL.Materia.GetAllSP(materia);

            string action = "http://tempuri.org/IMateria/GetAll";
            string url = "http://localhost:52731/Materia.svc";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:GetAll>
         <tem:materia>
            <ml:Nombre>{materia.Nombre}</ml:Nombre>
            <ml:Semestre>       
               <ml:IdSemestre>{materia.Semestre.IdSemestre}</ml:IdSemestre>
            </ml:Semestre>
         </tem:materia>
      </tem:GetAll>
   </soapenv:Body>
</soapenv:Envelope>";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string resultSOAP = reader.ReadToEnd();

                        // Deserializar el XML
                        var materiaSOAP = GetAllMaterias(resultSOAP); // Captura el objeto completo
                        ML.Result resultSemestres = BL.Materia.SemestreGetAll();
                        if (resultSemestres.Correct)
                        {
                            materiaSOAP.Semestre.Semestres = resultSemestres.Objects;
                        }
                        return View(materiaSOAP); // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            //MateriaReference.MateriaClient materiaSOAP = new MateriaReference.MateriaClient();

            //var respuesta = materiaSOAP.GetAll(materia);

            //if (respuesta.Correct)
            //{
            //    result.Correct = respuesta.Correct;
            //    result.Objects = respuesta.Objects.ToList();
            //}

            if (result.Correct)
            {
                materia.Materias = result.Objects;
                ML.Result resultSemestres = BL.Materia.SemestreGetAll();
                if (resultSemestres.Correct)
                {
                    materia.Semestre.Semestres = resultSemestres.Objects;
                }

            }

            ViewBag.Errores = TempData["Errores"];

            ViewBag.Test = "Texto";
            return View(materia);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Materia Materia, HttpPostedFileBase inptArchivo, string tipoArchivo)
        {

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



            if (tipoArchivo == null)
            {
                // Busqueda abierta
                //Materia.Nombre = Materia.Nombre == null ? "" : Materia.Nombre;
                Materia.Nombre = Materia.Nombre ?? "";


                Materia.Nombre = "";
                Materia.Semestre.IdSemestre = 0;
                //apellidos
                //Rol.IdRol = 0
            }
            else if (tipoArchivo == "txt")
            {
                //Path.GetFileName(inptArchivo.FileName)  txt, excel
                if (inptArchivo.FileName.Split('.')[1] == "txt")
                {
                    Materia.Semestre = new ML.Semestre();
                    ML.CargaMasiva cargaMasiva = new ML.CargaMasiva();
                    cargaMasiva.Errores = new List<string>();
                    cargaMasiva.Correctos = new List<string>();

                    using (StreamReader stream = new StreamReader(inptArchivo.InputStream))
                    {
                        string linea = stream.ReadLine();
                        while ((linea = stream.ReadLine()) != null)
                        {
                            string[] lineaLeida = linea.Split('|');
                            BL.CargaMasiva.Validar(lineaLeida, cargaMasiva.Errores, cargaMasiva.Correctos);
                        }

                        if (cargaMasiva.Errores.Count > 0)
                        {
                            string ruta = Path.GetFileName(inptArchivo.FileName);
                            string fullPath = Server.MapPath("~/Content/Errores/") + Path.GetFileNameWithoutExtension(inptArchivo.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
                            Session["Errores"] = fullPath;

                            if (!System.IO.File.Exists(fullPath))
                            {
                                using (StreamWriter streamWriter = new StreamWriter(fullPath))
                                {
                                    foreach (string lineaRead in cargaMasiva.Errores)
                                    {
                                        streamWriter.WriteLine(lineaRead);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string ruta = Path.GetFileName(inptArchivo.FileName);
                            string fullPath = Server.MapPath("~/Content/txt/") + Path.GetFileNameWithoutExtension(inptArchivo.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + ".txt";
                            Session["Correctos"] = fullPath;
                            if (!System.IO.File.Exists(fullPath))
                            {
                                inptArchivo.SaveAs(fullPath);
                            }
                        }
                    }

                    ViewBag.CargaMasiva = cargaMasiva;
                    ML.Materia materia = new ML.Materia();
                    TempData["Errores"] = "Mensaje desde el controlador";
                }


            }
            else
            {
                if (Path.GetExtension(inptArchivo.FileName) == ".xlsx")
                {
                    string path = Path.GetFileName(inptArchivo.FileName);
                    string fullPath = Server.MapPath("~/Content/Excel/") + Path.GetFileNameWithoutExtension(inptArchivo.FileName) + DateTime.Now.ToString("ddMMyyhhmmss") + ".xlsx";

                    if (!System.IO.File.Exists(fullPath))
                    {
                        inptArchivo.SaveAs(fullPath);
                    }

                    string conectionString = $"Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=\"Excel 12.0 Xml;HDR=YES\"; Data Source={fullPath};";

                    ML.Result ResultExcel = BL.CargaMasiva.LeerExcel(conectionString);

                }
            }

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

            if (ModelState.IsValid)
            {
                //todo esta bien

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

                if (ModelState.IsValid)
                {
                    //true
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
                }
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




        private ML.Materia GetAllMaterias(string xml)
        {
            var materia = new ML.Materia();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);

            // Acceder a GetAllUsuarioResult
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                var materiaObject = new ML.Materia();
                materiaObject.Semestre = new ML.Semestre();

                //byte[]
                int IdMateria;

                if (elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdMateria")?.Value != null)
                {
                    IdMateria = int.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdMateria")?.Value);
                }
                else
                {
                    IdMateria = 0;
                }

                //int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdMateria")?.Value, out IdMateria); //0
                materiaObject.IdMateria = IdMateria;

                // Acceso a otros campos
                materiaObject.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                materiaObject.Descripcion = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Descripcion")?.Value ?? string.Empty);

                decimal Creditos;

                if (elem.Element("{http://schemas.datacontract.org/2004/07/ML}Creditos")?.Value != null)
                {
                    Creditos = decimal.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Creditos")?.Value);
                }
                else
                {
                    Creditos = 0;
                }
                materiaObject.Creditos = Creditos;

                materiaObject.Fecha = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Fecha")?.Value ?? string.Empty);


                int IdSemestre;

                if (elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdSemestre")?.Value != null)
                {
                    IdSemestre = int.Parse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdMateria")?.Value);
                }
                else
                {
                    IdSemestre = 0;
                }

                var Imagen = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Imagen")?.Value;

                if (!string.IsNullOrEmpty(Imagen))
                {
                    materiaObject.Imagen = Convert.FromBase64String(Imagen);
                }
                else
                {
                    materiaObject.Imagen = null;
                }

                materiaObject.Semestre.IdSemestre = IdSemestre;
                result.Objects.Add(materiaObject);
            }


            materia.Materias = result.Objects;

            return materia;
        }




    }
}