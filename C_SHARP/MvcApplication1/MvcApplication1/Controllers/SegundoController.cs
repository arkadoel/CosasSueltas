using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MvcApplication1.Controllers
{
    public class SegundoController : Controller
    {

        
        //
        // GET: /Segundo/

        public ActionResult Index()
        {
            ViewData["Datos"] = "Bienvenido";
            return View();
        }

        public ActionResult guarda(String datos)
        {
            DataLogic dat= new DataLogic();
            dat.listarPersonas();
            return RedirectToAction("Index");
        }

        //
        // GET: /Segundo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Segundo/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Segundo/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Segundo/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Segundo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Segundo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Segundo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewJSON()
        {
            return View();

        }

        /// <summary>
        /// Se puede llamar con http://<site/>/Segundo/runjson
        /// </summary>
        /// <returns></returns>
        public JsonResult runjson()
        {
            string output = "hacer json";

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Muestra la cadena JSON con las personas guardadas en la base de datos
        /// </summary>
        /// <returns></returns>
        public JsonResult verPersonas()
        {
            List<Persona> personas = new DataLogic().listarPersonas();
            return Json(personas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Para llamarlo poner /Segundo/calcular?numero=...
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public JsonResult calcular(int numero)
        {
            int resultado = 0;
            resultado += numero * 3;
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult procesar(string v1, string v2, string v3, string v4)
        {
            
            List<Persona> personas = new DataLogic().listarPersonas();
            return Json(personas, JsonRequestBehavior.AllowGet); ;
        }
                
        public JsonResult ProcesarObjetos(string sjson)
        {
            //tenemos una cadena json, ahora pasarla a objeto
            Persona datos = JsonConvert.DeserializeObject<Persona>(sjson);

            //adivinemos su id
            List<Persona> personas = new DataLogic().listarPersonas();
            var persona = (from p in personas
                           where p.Nombre.Contains( datos.Nombre) 
                           select p).First();

            return Json(persona);
        }


        public JsonResult ProcesarListaObjetos(string sjson)
        {
            //tenemos una cadena json, ahora pasarla a objeto lista
            List<Persona> datos = JsonConvert.DeserializeObject<List<Persona>>(sjson);


            return Json(datos);
        }
         
    }
}
