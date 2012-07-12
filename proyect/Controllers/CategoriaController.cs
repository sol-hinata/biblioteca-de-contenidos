using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
namespace proyect.Controllers
{
    public class CategoriaController : Controller
    {
        //
        // GET: /Categoria/

        public ActionResult Index()
        {
            DataClasses1DataContext db= new DataClasses1DataContext();
            List<Categoria> cp = db.categorias.Where(q => q.estado == true).Select(x => new Categoria() { nombre = x.nombre }).ToList();
            return View(cp);
        }
        public ActionResult getCategoria()
        {
            int p=3;
            DataClasses1DataContext db = new DataClasses1DataContext();
            Articulo info = db.articulos.Select(a => new Articulo() {titulo = a.titulo , categorialista = db.categorias.Select(b => new categoriapublicacion() {id=b.idCategoria,nombre=b.nombre}).ToList() }).ToArray()[0];
            ViewBag.c = info;
            return View();
        }
        public ActionResult Detalles(int id){
             DataClasses1DataContext db = new DataClasses1DataContext();
           // var categoria = new Categoria { nombre = "categoria" + id };
            var categoria = db.categorias.Where(u => u.idCategoria == id).ToArray()[0];
            return View (categoria);
        }
        public ActionResult Browse(string cate) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var cateModel = new Categoria { nombre = cate };
            var cateModel1 = db.categorias.Single(g => g.nombre == cate);
            return View(cateModel1);
        }
        public ActionResult Resultados(string cate)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();


            List<articulo> arc = db.publicacion_categorias.Where(a => a.categoria.nombre == cate).Where(a => a.idPublicacion == a.publicacion.articulo.idPublicacion).Where(a => a.publicacion.estado == true).Select(a => a.publicacion.articulo).ToList();
            List<tutorial> tuc = db.publicacion_categorias.Where(a => a.categoria.nombre == cate).Where(a => a.idPublicacion == a.publicacion.tutorial.idPublicacion).Where(a => a.publicacion.estado == true).Select(a => a.publicacion.tutorial).ToList();
            List<libro> lic = db.publicacion_categorias.Where(a => a.categoria.nombre == cate).Where(a => a.idPublicacion == a.publicacion.libro.idPublicacion).Where(a => a.publicacion.estado == true).Select(a => a.publicacion.libro).ToList();
            List<curso> cuc = db.publicacion_categorias.Where(a => a.categoria.nombre == cate).Where(a => a.idPublicacion == a.publicacion.curso.idPublicacion).Where(a => a.publicacion.estado == true).Select(a => a.publicacion.curso).ToList();
            ViewBag.i = arc;
            ViewBag.t = tuc;
            ViewBag.l = lic;
            ViewBag.c = cuc; 

           
            return View();
        }
    }
}
