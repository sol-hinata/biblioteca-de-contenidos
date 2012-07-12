using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security;

namespace proyect.Models
{
    public class Publicacion
    {
        public int idPublicacion { get; set; }
        public System.Guid id { get; set; }
        public int estado { get; set; }
        public System.DateTime fecha { get; set; }
        public string observaciones { get; set; }
    }
    public class Articulo
    {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        [Required]
        [Display(Name = "detalle")]
        [UIHint("tinymce_jquery_full"),AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public System.DateTime fecha { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        [Required]
        [Display(Name = "nombrecate")]
        public string nombrecate { set; get; }
        public int idcat { set; get; }
    }
    public class verarticulo {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public string nombre { get; set; }
        public string nombrecom { get; set; }
        public string avatar { get; set; }
        public string cat { get; set; }
        public System.Guid id { get; set; }
        public double puntaje { get; set; }
        public List<perfil> perf { set; get; }
        public List<avatar> ava { get; set; }
        public string intereses { get; set; }
        public List<archivo> archivo { set; get; }
        public List<categoria> categoria { set; get; }
    
    }
    public class listapendientearticulo {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public string nombre { get; set; }
        public List<archivo> archivo { set; get; }
    }
    public class publicar {
        public int idPublicacion { get; set; }
        public int estado { get; set; }
        public int puntuacion { get; set; }
        public System.Guid id { get; set; }
        public String detalle { get; set; }
        public double total { get; set; }
        public System.DateTime fecha { get; set; }

    }
    //AUMENTAR VERCONTENIDO
    //LIBRO
    public class Libro
    {
        public int idPublicacion { get; set; }
        public int idArchivo { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public string categoria { get; set; }
        [Required]
        [Display(Name = "detalle")]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        [Required]
        [Display(Name = "autor")]
        public string Autor { get; set; }
        [Required]
        [Display(Name = "año de publicacion")]
        public int Annio_pub { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }

    }
    
    public class Archivo
    {
        public int idArchivo { get; set; }
        public int idPublicacion { get; set; }
        public string rutafisica { get; set; }
        public string rutavirtual { get; set; }
        public System.DateTime fecha { get; set; }
    }
    public class listapendientelibro
    {
        public int idPublicacion { get; set; }
        public System.Guid id { set; get; }
        public string nombre { set; get; }
        public string rutavirtual { get; set; }
        public string titulo { set; get; }
        public List<archivo> archivo { set; get; }
    }
    public class VerLibro {
        public int idPublicacion { get; set; }
        public System.Guid id { set; get; }
        public string nombre { set; get; }
        public string detalle { set; get; }
        public string titulo { set; get; }
        public DateTime fecha { get; set; }
        public int Annio_pub { get; set; }
        public string Autor { get; set; }
        public string avas { get; set; }
        public List<libro> libro { set; get; }
        public List<perfil> perf { set; get; }
        public List<archivo> archivo { set; get; }
        public List<avatar>ava { get; set; }
        public string cat { get; set; }
        public double puntaje { get; set; }
        public List<categoria> categoria { set; get; }
        public string intereses { get; set; }
        public List<articulo> cantart { get; set; }
        public int cantlib { get; set; }
        public int cantcur { get; set; }
        public int canttuto { get; set; }
    }

    public class Curso {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public List<categoriapublicacion> categorialista { set; get; }
        public string nombrecate { set; get; }
        public int idcat { set; get; }
    }
    public class listapendientecurso
    {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public string nombre { get; set; }
    }
    public class vercurso {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public string nombre { get; set; }
        public string nombrecom { get; set; }
        public string avatar { get; set; }
        public string cat { get; set; }
        public System.Guid id { get; set; }
        public double puntaje { get; set; }
        public List<perfil> perf { set; get; }
        public List<avatar> ava { get; set; }
        public string intereses { get; set; }
        public List<archivo> archivo { set; get; }
        public List<categoria> categoria { set; get; }
    }
    public class Tutorial {
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "titulo")]
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public List<categoriapublicacion> categorialista { get; set; }
        public string nombrecate { get; set; }
        public int idcat { get; set; }
    }
    public class listapendientetutorial
    {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public string nombre { get; set; }
    }
    public class vertutorial
    {
        public int idPublicacion { get; set; }
        public string titulo { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalle { get; set; }
        public int puntuacion { get; set; }
        public string nombre { get; set; }
        public string nombrecom { get; set; }
        public string avatar { get; set; }
        public List<categoria> categoria { set; get; }
        public string cat { get; set; }
        public System.Guid id { get; set; }
        public double puntaje { get; set; }
        public List<archivo> archivo { set; get; }
        public List<perfil> perf { set; get; }
        public List<avatar> ava { get; set; }
        public string intereses { get; set; }

    }
    public class observarcion {
        public int idPublicacion { get; set; }
        public string observaciones { get; set; }
    }

    public class mostrararticulos {
        public int idPublicacion { get; set; }
        public int idPerfil { get; set; }
        public System.Guid id { get; set; }
        public System.Guid idus { get; set; }
        public string titulo { get; set; }
        public string detalle { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public string[] nomcat { get; set; }
        public List<publicacion_categoria> rel { get; set; }
        public List<categoria> cate { get; set; }
        public double puntuacion { get; set; }
        public List<archivo> archivo { get; set; }
        public List<avatar> av { get; set; }
        public List<comentario> comentario { get; set; }
        public int like { get; set; }
        public List<categoria> categ { get; set; }
        public List<Categoria> categoria { get; set; }
    }
    public class mostrarcursos
    {
        public int idPublicacion { get; set; }
        public System.Guid id { get; set; }
        public System.Guid idus { get; set; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public string detalle { get; set; }
        public DateTime fecha { get; set; }
        public List<Categoria> categoria { get; set; }
        public double puntuacion { get; set; }
        public List<archivo> archivo { get; set; }
        public List<avatar> av { get; set; }
        public List<comentario> comentario { get; set; }
        public int like { get; set; }
        public categoria categ { get; set; }
        public publicacion_categoria categg { get; set; }

    }
    public class mostrartutoriales
    {
        public int idPublicacion { get; set; }
        public int idPerfil { get; set; }
        public System.Guid id { get; set; }
        public System.Guid idus { set; get; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public string detalle { get; set; }
        public DateTime fecha { get; set; }
        public List<Categoria> categoria { get; set; }
        public double puntuacion { get; set; }
        public List<archivo> archivo { get; set; }
        public List<avatar> av { get; set; }
        public List<comentario> comentario { get; set; }
        public int like { get; set; }

    }
    public class mostrarlibros {
        public int idPublicacion { get; set; }
        public int idPerfil { get; set; }
        public System.Guid idus { set; get; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public int Annio_pub { get; set; }
        public string Autor { get; set; }
        public List<Categoria> categoria { get; set; }
        public double puntuacion { get; set; }
        public List<archivo> archivo { get; set; }
        public List<avatar> av { get; set; }
        public List<comentario> comentario { get; set; }
        public int like { get; set; }
    }
    public class Karma2 {
        public System.Guid UserId { get; set; }
        public int idPublicacion { get; set; }
    }
    //aumento
    public class Comentario {
        public System.Guid UserId { get; set; }
        public int idPublicacion { get; set; }
        [Required]
        [Display(Name = "relice un comentario")]
        public string Descripcion { get; set; }
        public float puntuacion { get; set; }
        public DateTime fecha { get; set; }
    }
    public class vercomentario
    {
        public int idComentario { get; set; }
        public System.Guid id { get; set; }
        public int idPublicacion { get; set; }
        public string Descripcion { get; set; }
        public double puntaje { get; set; }
        public System.DateTime fecha { get; set; }
        public string nick { get; set; }
        public List<karma> puntuacion { get; set; }
        public List<aspnet_User> nombre { get; set; }
        public List<avatar> ava { get; set; }
    }
}