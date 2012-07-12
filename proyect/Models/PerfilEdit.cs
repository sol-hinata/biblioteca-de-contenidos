using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyect.Models
{
    public class PerfilEdit
    {
        public int idPerfil { get; set; }
        public int idAvatar { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string apellidom { get; set; }
        public string interes { get; set; }
        public string ubicacion { get; set; }
        public System.DateTime fecha { get; set; }
        public System.Guid id { get; set; }
        public int estado { get; set; }
    }
    public class Avatar
    {
        public int idAvatar { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }
        public System.Guid UseId { get; set; }

    }
    public class foto {
        public System.Guid id { get; set; }
        public int idPerfil { get; set; }
        public string ava { get; set; }
        public string rutafisica { get; set; }
        public List<avatar> fot { set; get; }
    
    }
}