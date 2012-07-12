using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class DetalleactividadController : Controller
    {
        //
        // GET: /Detalleactividad/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var listaarti = db.publicacions.Where(a => a.UserId == (Guid)Session["ids"]).Select( b=>b.aspnet_User.karma.total).ToArray()[0];
            @ViewBag.l = listaarti;

            List<mostrararticulos> listarti = db.publicacions.Where(tt=>tt.UserId==(Guid)Session["ids"]).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
               // archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.articulo.titulo,

               // detalle = a.articulo.detalle,
                 puntuacion=a.articulo.puntuacion,
                 //total=a.aspnet_User.karma.total,
                 

                
                //  categ = db.categorias.Where(q => q.estado == true).Select(x => new Categoria() { nombre = x.nombre }).ToList()
                //nomcat = a.publicacion_categorias.Where(j => j.categoria.estado == true).Where(i => i.idCategoria == i.categoria.idCategoria).Select(k =>k.categoria.nombre).ToList(),
            }).ToList();
            ViewBag.listaarticulo = listarti;
            //10 ultimos tutoriales
            List<mostrartutoriales> listtuto = db.publicacions.Where(a => a.UserId == (Guid)Session["ids"]).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.tutorial.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.tutorial.titulo,
                detalle = a.tutorial.detalle.Substring(1, 5)
            }).ToList();
            ViewBag.listatutorial = listtuto;
            //10 ultimos cursos
            List<mostrarcursos> listcurso = db.publicacions.Where(a => a.UserId == (Guid)Session["ids"]).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.curso.titulo,
                detalle = a.curso.detalle.ToString().Substring(1, 2)
            }).ToList();
            ViewBag.listacurso = listcurso;
            //10 ultimos libros
            List<mostrarlibros> listlibro = db.publicacions.Where(a => a.UserId == (Guid)Session["ids"]).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.libro.idPublicacion).Take(10).Select(a => new mostrarlibros()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.libro.titulo,
                descripcion = a.libro.detalle
            }).ToList();
            ViewBag.listalibro = listlibro;

           
            
            return View();
        }

    }
}
