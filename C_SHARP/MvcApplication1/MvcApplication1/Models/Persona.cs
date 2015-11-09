using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    [Serializable]
    public class Persona
    {
        public int? ID { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public char Genero { get; set; }
    }
}