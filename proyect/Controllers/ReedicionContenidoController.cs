using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    public class ReedicionContenidoController : Controller
    {
        //
        // GET: /ReedicionContenido/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<Publicacion> publi = db.publicacions.Where( p=>p.UserId==(Guid)Session["ids"]&& p.estado == false).Select(pp => new Publicacion { idPublicacion = pp.idPublicacion, fecha = pp.fecha, observaciones = pp.observaciones }).ToList();
            ViewBag.lista = publi;
            return View();
        }
        public ActionResult Reeditar(int id) {
            int i = id;
            DataClasses1DataContext db = new DataClasses1DataContext();
             if (db.libros.Where(v => v.idPublicacion == i).ToList().Count != 0) {
                return RedirectToAction("ReeditarLibro", "ReedicionContenido", new { id = id }); 
            }
            if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("ReeditarCurso", "ReedicionContenido", new { id = id });    
            }
            if (db.articulos.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {   
                return RedirectToAction("ReeditarArticulo", "ReedicionContenido", new { id = id });
                //return View(info);
            }
            if (db.tutorials.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("ReeditarTutorial", "ReedicionContenido", new { id = id });
                //return View(info);
            }
            //ViewBag.detalle = info.detalle;
            return View();
        }
        public ActionResult ReeditarCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Curso data = db.cursos.Where(d => d.idPublicacion == id).Select(d => new Curso() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            Curso info = data;
            return View(info);
        }
        [HttpPost]
        public ActionResult ReeditarCurso(int id,Curso model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var c = db.cursos.Where(i => i.idPublicacion == id);
            curso cu = new curso() { titulo = model.titulo, detalle = model.detalle, fecha = DateTime.Now };
            c.ToArray()[0].titulo = model.titulo;
            c.ToArray()[0].detalle = model.detalle;
            c.ToArray()[0].fecha = DateTime.Now;
            db.SubmitChanges();
            return RedirectToAction("index", "Home");
            
        }
        public ActionResult ReeditarArticulo(int id) {
            DataClasses1DataContext db= new DataClasses1DataContext();
            Articulo d = db.articulos.Where(o => o.idPublicacion == id).Select(z => new Articulo() { titulo = z.titulo, detalle = z.detalle }).ToArray()[0];
            Articulo info = d;
            return View(info);

        }
        [HttpPost]
        public ActionResult ReeditarArticulo(int id,Articulo model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var c = db.articulos.Where(i => i.idPublicacion == id);
            articulo cu = new articulo() { titulo = model.titulo, detalle = model.detalle, fecha = DateTime.Now };
            c.ToArray()[0].titulo = model.titulo;
            c.ToArray()[0].detalle = model.detalle;
            c.ToArray()[0].fecha = DateTime.Now;
            db.SubmitChanges();
            return RedirectToAction("index", "Home");
        }
        public ActionResult ReeditarTutorial(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            Tutorial data = db.tutorials.Where(p => p.idPublicacion == id).Select(d => new Tutorial() { titulo = d.titulo, detalle = d.detalle }).ToArray()[0];
            Tutorial info = data;
            return View(info);
        }
        [HttpPost]
        public ActionResult ReeditarTutorial(int id, Tutorial model) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var c = db.tutorials.Where(i => i.idPublicacion == id);
            tutorial cu = new tutorial() { titulo = model.titulo, detalle = model.detalle, fecha = DateTime.Now };
            c.ToArray()[0].titulo = model.titulo;
            c.ToArray()[0].detalle = model.detalle;
            c.ToArray()[0].fecha = DateTime.Now;
            db.SubmitChanges();
            return RedirectToAction("index", "Home");
        }
        public ActionResult ReeditarLibro(int id) {
            
            DataClasses1DataContext db = new DataClasses1DataContext();
            Libro data = db.libros.Where(p => p.idPublicacion == id).Select(d => new Libro() { titulo = d.titulo, Autor = d.Autor, detalle = d.detalle }).ToArray()[0];
            Libro info = data;
            ViewBag.im = data;
            return View(info);
        }
         [HttpPost]
        public ActionResult ReeditarLibro(int id, Libro model)
        {
                DataClasses1DataContext db = new DataClasses1DataContext();
                var l = db.libros.Where(li => li.idPublicacion == id);
                libro libr = new libro() { titulo = model.titulo, fecha = DateTime.Now, Autor = model.Autor, Annio_pub = model.Annio_pub, detalle = model.detalle};
                l.ToArray()[0].titulo = model.titulo;
                l.ToArray()[0].fecha = DateTime.Now;
                l.ToArray()[0].Autor = model.Autor;
                l.ToArray()[0].Annio_pub = model.Annio_pub;
                l.ToArray()[0].detalle = model.detalle;
                db.SubmitChanges();
            return View();
        }


        public ActionResult Borrar(int id) {

            return View();
        }
        
    }
}
