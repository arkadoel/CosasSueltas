using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2WebAPI.API
{
    public class PersonasController : ApiController
    {
        public class respuesta1
        {
            public int ID { get; set; }
            public string NOMBRE { get; set; }
        }
        // GET: api/Personas
        public List<respuesta1> Get()
        {
            List<respuesta1> salida = new List<respuesta1>();
            string respuesta = "[";

            for (int i = 0; i < 50; i++)
            {
                salida.Add( new respuesta1(){ ID = i, NOMBRE = "nombre" + i.ToString() });
            }

            /*respuesta = respuesta.Substring(0, respuesta.Length - 1);
            respuesta += "]";*/
            return salida;
        }

        // GET: api/Personas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Personas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Personas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Personas/5
        public void Delete(int id)
        {
        }
    }
}
