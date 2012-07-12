using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Globalization;
using System.Security;

namespace proyect.Models
{
    public class Usuario
    {
        public System.Guid idus { get; set; }
        public string nom { get; set; }
        public string r { get; set; }
        public List<aspnet_Role> roles { get; set; }
        public List<perfil> per { get; set; }
        public List<avatar> ava { get; set; }
        public double puntaje { get; set; }
    }
     public class VerUsuarios
    {
         public System.Guid idus { get; set; }
        public string nombre { get; set; }
        public string r { get; set; }
        public List<avatar> ava { get; set; }
        public DateTime fechar { get; set; }
        public DateTime fechau { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
     public class Buscar
     {
         [Required]
         [Display(Name = "usuario")]
         public string palabra { get; set; }
         public string buscado { get; set; }
     }
}