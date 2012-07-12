using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class ModeracionCategoriaController : Controller
    {
        //
        // GET: /ModeracionCategoria/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<Categoria> listcategorias = db.categorias.Where(es => es.estado == false).Select(a => new Categoria() { idCategoria = a.idCategoria, nombre = a.nombre }).ToList();
            ViewBag.lista = listcategorias;
            return View();
        }
     
        public ActionResult PublicarCategoria(int idc)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var cat = db.categorias.Where(a => a.idCategoria == idc);
            categoria c = new categoria() { estado = true };
            cat.ToArray()[0].estado = true;
            db.SubmitChanges();

            return RedirectToAction("Index", "ModeracionContenido");
        }

        public ActionResult BorrarCategoria(int idc)
        {   DataClasses1DataContext db = new DataClasses1DataContext();
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == idc).ToArray()[0];
            int i = ip.idPublicacion;
            if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("CategoriaCurso", "ModeracionCategoria", new { id = idc });
            }
            if (db.articulos.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("CategoriaArticulo", "ModeracionCategoria", new { id = idc });
                //return View(info);
            }
            if (db.libros.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("CategoriaLibro", "ModeracionCategoria", new { id = idc });
            }

            if (db.tutorials.Where(v => v.idPublicacion == i).ToList().Count != 0)
            {
                return RedirectToAction("CategoriaTutorial", "ModeracionCategoria", new { id = idc });
                //return View(info);
            }
            ViewBag.ve = i;
            return View();
        }
       
        public ActionResult CategoriaCurso(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<categoria> ca = db.categorias.Where(u=>u.estado==true).ToList();
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == id).ToArray()[0];
            int i = ip.idPublicacion;
            Curso data = db.cursos.Where(c => c.idPublicacion == i).Select(d => new Curso() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            ViewBag.cat = ca;
            ViewBag.infoa = data;
            ViewBag.idc = id;
            return View();   
        }
        [HttpPost]
        public ActionResult CategoriaCurso(int idc, Categoria model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            categoria ic = db.categorias.Where(c => c.nombre == model.nombre).ToArray()[0];
            //busco donde voy a actualizar
            
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria ==model.idc ).ToArray()[0];
            int i = ip.idPublicacion;
            int ipubcate = ip.idCategoria;
            //inserta otra relacion
            publicacion_categoria ca = new publicacion_categoria() { idCategoria = ic.idCategoria, idPublicacion = i };
            db.publicacion_categorias.InsertOnSubmit(ca);
            db.SubmitChanges();

            //elimina la fila publicacion_categoria
            publicacion_categoria categ = db.publicacion_categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.publicacion_categorias.DeleteOnSubmit(categ);
            db.SubmitChanges();
            //elimina la categoria de la tabla
            categoria cate = db.categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.categorias.DeleteOnSubmit(cate);
            db.SubmitChanges();
       
            return RedirectToAction("Index", "ModeracionCategoria");
            
        }
        public ActionResult CategoriaArticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<categoria> ca = db.categorias.Where(u => u.estado == true).ToList();
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == id).ToArray()[0];
            int i = ip.idPublicacion;
            Articulo data = db.articulos.Where(c => c.idPublicacion == i).Select(d => new Articulo() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            ViewBag.cat = ca;
            ViewBag.infoa = data;
            ViewBag.idc = id;
            return View();
        }
        [HttpPost]
        public ActionResult CategoriaArticulo(int idc,Categoria model) {
            DataClasses1DataContext db = new DataClasses1DataContext();

            categoria ic = db.categorias.Where(c => c.nombre == model.nombre).ToArray()[0];
            //busco donde voy a actualizar

            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == model.idc).ToArray()[0];
            int i = ip.idPublicacion;
            int ipubcate = ip.idCategoria;
            //inserta otra relacion
            publicacion_categoria ca = new publicacion_categoria() { idCategoria = ic.idCategoria, idPublicacion = i };
            db.publicacion_categorias.InsertOnSubmit(ca);
            db.SubmitChanges();

            //elimina la fila publicacion_categoria
            publicacion_categoria categ = db.publicacion_categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.publicacion_categorias.DeleteOnSubmit(categ);
            db.SubmitChanges();
            //elimina la categoria de la tabla
            categoria cate = db.categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.categorias.DeleteOnSubmit(cate);
            db.SubmitChanges();

            return RedirectToAction("Index", "ModeracionCategoria");
        }
        public ActionResult CategoriaTutorial(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<categoria> ca = db.categorias.Where(u => u.estado == true).ToList();
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == id).ToArray()[0];
            int i = ip.idPublicacion;
            Tutorial data = db.tutorials.Where(c => c.idPublicacion == i).Select(d => new Tutorial() { titulo = d.titulo, detalle = d.detalle, fecha = d.fecha }).ToArray()[0];
            ViewBag.cat = ca;
            ViewBag.infoa = data;
            ViewBag.idc = id;
            return View();

        }
        [HttpPost]
        public ActionResult CategoriaTutorial(int idc,Categoria model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            categoria ic = db.categorias.Where(c => c.nombre == model.nombre).ToArray()[0];
            //busco donde voy a actualizar

            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == model.idc).ToArray()[0];
            int i = ip.idPublicacion;
            int ipubcate = ip.idCategoria;
            //inserta otra relacion
            publicacion_categoria ca = new publicacion_categoria() { idCategoria = ic.idCategoria, idPublicacion = i };
            db.publicacion_categorias.InsertOnSubmit(ca);
            db.SubmitChanges();

            //elimina la fila publicacion_categoria
            publicacion_categoria categ = db.publicacion_categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.publicacion_categorias.DeleteOnSubmit(categ);
            db.SubmitChanges();
            //elimina la categoria de la tabla
            categoria cate = db.categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.categorias.DeleteOnSubmit(cate);
            db.SubmitChanges();

            return RedirectToAction("Index", "ModeracionCategoria");
        }
        public ActionResult CategoriaLibro(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<categoria> ca = db.categorias.Where(u => u.estado == true).ToList();
            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == id).ToArray()[0];
            int i = ip.idPublicacion;
            Libro data = db.libros.Where(c => c.idPublicacion == i).Select(d => new Libro() { titulo = d.titulo, Autor = d.Autor, Annio_pub = (int)d.Annio_pub, fecha = d.fecha, detalle = d.detalle }).ToArray()[0];
            ViewBag.cat = ca;
            ViewBag.infoa = data;
            ViewBag.idc = id;
            return View();

        }
        [HttpPost]
        public ActionResult CategoriaLibro(int idc,Categoria model)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            categoria ic = db.categorias.Where(c => c.nombre == model.nombre).ToArray()[0];
            //busco donde voy a actualizar

            publicacion_categoria ip = db.publicacion_categorias.Where(a => a.idCategoria == model.idc).ToArray()[0];
            int i = ip.idPublicacion;
            int ipubcate = ip.idCategoria;
            //inserta otra relacion
            publicacion_categoria ca = new publicacion_categoria() { idCategoria = ic.idCategoria, idPublicacion = i };
            db.publicacion_categorias.InsertOnSubmit(ca);
            db.SubmitChanges();

            //elimina la fila publicacion_categoria
            publicacion_categoria categ = db.publicacion_categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.publicacion_categorias.DeleteOnSubmit(categ);
            db.SubmitChanges();
            //elimina la categoria de la tabla
            categoria cate = db.categorias.Where(p => p.idCategoria == idc).ToArray()[0];
            db.categorias.DeleteOnSubmit(cate);
            db.SubmitChanges();

            return RedirectToAction("Index", "ModeracionCategoria");
        }
        
    }
}
