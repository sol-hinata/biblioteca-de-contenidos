using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyect.Models
{
    public class categoriapublicacion
    {
        public int id { set; get; }
        public string nombre { set; get; }
    }
    public class Categoria {
        public int idCategoria { get; set; }
        public int idc { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }
    }
    public class Publicacion_categoria {
        public int idCategoria { get; set; }
        public int idPublicacion { get; set; }
    }
}