using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //10 ultimos articulos
            List<mostrararticulos> listarti = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                archivo = a.archivos.ToList(),
               categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre=w.categoria.nombre}).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.articulo.titulo,
                detalle = a.articulo.detalle
            }).ToList();
            ViewBag.listaarticulo = listarti;
           
           //10 ultimos tutoriales
            List<mostrartutoriales> listtuto = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.tutorial.idPublicacion).Take(10).Select(a => new mostrartutoriales()
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
            List<mostrarcursos> listcurso = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
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
            List<mostrarlibros> listlibro = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.libro.idPublicacion).Take(10).Select(a => new mostrarlibros()
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

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Varticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<verarticulo> dataA = db.articulos.Where(d => d.idPublicacion == id).Select(d => new verarticulo()
            {
                idPublicacion = id, 
                archivo = d.publicacion.archivos.ToList(),
                nombre = d.publicacion.aspnet_User.UserName,
                perf = d.publicacion.aspnet_User.perfils.ToList(),
                ava = d.publicacion.aspnet_User.avatars.OrderByDescending(i => i.idAvatar).ToList(),
                puntaje = d.publicacion.aspnet_User.karma.total,
                fecha = d.fecha,
              titulo = d.titulo,
              detalle = d.detalle }).ToList(); 
            ViewBag.arti = dataA;
            ViewBag.id = id;

            //recuperamos el usuario q ha publicado para obtener sus 10 publicaciones-articulo
            System.Guid idus = db.publicacions.Where(g => g.idPublicacion == id).Select(f => f.UserId).ToArray()[0];
            List<mostrararticulos> lisusua = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.articulo.titulo,
            }).ToList();
            ViewBag.lua = lisusua;
            List<mostrartutoriales> lisusut = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.tutorial.titulo,
            }).ToList();
            ViewBag.lut = lisusut;
            List<mostrarcursos> lisusuc = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.curso.titulo,
            }).ToList();
            ViewBag.luc = lisusuc;
            List<mostrarlibros> lisusul = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarlibros()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.libro.titulo,
            }).ToList();
            ViewBag.lul = lisusul;
            
            
            //mostrar cant de comentario
            var cantcomen = db.comentarios.Where(i => i.idPublicacion == id).ToList().Count();
            ViewBag.cantidadc = cantcomen;
            //mostrar comentarios 
            List<vercomentario> todoscometarios = db.comentarios.Where(i => i.idPublicacion == id).Where(o => o.aspnet_User.UserId == o.UserId).Select(f => new vercomentario()
            {
                nick = f.aspnet_User.UserName,
                ava = f.aspnet_User.avatars.ToList(),
                Descripcion = f.Descripcion,
                fecha = f.fecha,
                puntaje = f.aspnet_User.karma.total
            }).ToList();
            ViewBag.todos = todoscometarios;

            //categoria
            List<Categoria> c = db.publicacion_categorias.Where(u => u.idPublicacion == id).Select(i => new Categoria() { nombre = i.categoria.nombre, idCategoria=i.idCategoria }).ToList();
            ViewBag.c= c;
            System.Guid idusu = db.publicacions.Where(i => i.idPublicacion == id).Select(a => a.aspnet_User.UserId).ToArray()[0];
            
            //cantidad de articulos
            var num = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(t => t.articulo.idPublicacion).ToList();
            ViewBag.ca = num.Count();
            //cantidad de libros
            var numl = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(t => t.libro.idPublicacion).ToList();
            ViewBag.cl = numl.Count();
            //cantidad de tutoriales
            var numt = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(t => t.tutorial.idPublicacion).ToList();
            ViewBag.ct = numt.Count();
            //cantidad de cursos
            var numc = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(t => t.curso.idPublicacion).ToList();
            ViewBag.cc = numc.Count();
            //mostrar comentarios  
            return View();
        }
        public ActionResult Aportes(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            System.Guid idusu=db.publicacions.Where(u=>u.idPublicacion==id).Select(t=>t.UserId).ToArray()[0];

            //sus ultimas dies 
            List<Publicacion> p = db.publicacions.Where(b => b.UserId == idusu).Where(e => e.estado == true).OrderByDescending(y => y.idPublicacion).Take(10).Select(u => new Publicacion() { idPublicacion = u.idPublicacion }).ToList();
            ViewBag.dies = p.Count();

            foreach (var i in p)
            {

                List<Articulo> a = db.articulos.Where(h => h.idPublicacion == i.idPublicacion).Select(j => new Articulo() { titulo = j.titulo , idPublicacion=j.idPublicacion}).ToList();
                ViewBag.a = a;
                List<Libro> l = db.libros.Where(h => h.idPublicacion == i.idPublicacion).Select(j => new Libro() { titulo = j.titulo, idPublicacion = j.idPublicacion }).ToList();
                ViewBag.l = l;
                List<Curso> c = db.cursos.Where(h => h.idPublicacion == i.idPublicacion).Select(j => new Curso() { titulo = j.titulo, idPublicacion = j.idPublicacion }).ToList();
                ViewBag.c = c;
                List<Tutorial> t = db.tutorials.Where(h => h.idPublicacion == i.idPublicacion).Select(j => new Tutorial() { titulo = j.titulo, idPublicacion = j.idPublicacion }).ToList();
                ViewBag.t = t;
            }
            return View();
        }
        public ActionResult Vcurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vercurso> dataA = db.cursos.Where(d => d.idPublicacion == id).Select(d => new vercurso()
            {
                idPublicacion = id,
                archivo = d.publicacion.archivos.ToList(),
                nombre = d.publicacion.aspnet_User.UserName,
                perf = d.publicacion.aspnet_User.perfils.ToList(),
                ava = d.publicacion.aspnet_User.avatars.OrderByDescending(i => i.idAvatar).ToList(),
                puntaje = d.publicacion.aspnet_User.karma.total,
                fecha = d.fecha,
                titulo = d.titulo,
                detalle = d.detalle
            }).ToList();
            ViewBag.cur = dataA;
            ViewBag.id = id;
            //recuperamos el usuario q ha publicado para obtener sus 10 publicaciones-articulo
            System.Guid idus = db.publicacions.Where(g => g.idPublicacion == id).Select(f => f.UserId).ToArray()[0];
            List<mostrararticulos> lisusua = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.articulo.titulo,
            }).ToList();
            ViewBag.lua = lisusua;
            List<mostrartutoriales> lisusut = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.tutorial.titulo,
            }).ToList();
            ViewBag.lut = lisusut;
            List<mostrarcursos> lisusuc = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.curso.titulo,
            }).ToList();
            ViewBag.luc = lisusuc;
            List<mostrarlibros> lisusul = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarlibros()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.libro.titulo,
            }).ToList();
            ViewBag.lul = lisusul;

            //mostrar cant de comentario
            var cantcomen = db.comentarios.Where(i => i.idPublicacion == id).ToList().Count();
            ViewBag.cantidadc = cantcomen;
            //mostrar comentarios 
            List<vercomentario> todoscometarios = db.comentarios.Where(i => i.idPublicacion == id).Where(o => o.aspnet_User.UserId == o.UserId).Select(f => new vercomentario()
            {
                nick = f.aspnet_User.UserName,
                ava = f.aspnet_User.avatars.ToList(),
                Descripcion = f.Descripcion,
                fecha = f.fecha,
                puntaje = f.aspnet_User.karma.total
            }).ToList();
            ViewBag.todos = todoscometarios;

            //categoria
            List<Categoria> c = db.publicacion_categorias.Where(u => u.idPublicacion == id).Select(i => new Categoria() { nombre = i.categoria.nombre, idCategoria = i.idCategoria }).ToList();
            ViewBag.c = c;
            System.Guid idusu = db.publicacions.Where(i => i.idPublicacion == id).Select(a => a.aspnet_User.UserId).ToArray()[0];
            //cantidad de articulos
            var num = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(t => t.articulo.idPublicacion).ToList();
            ViewBag.ca = num.Count();
            //cantidad de libros
            var numl = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(t => t.libro.idPublicacion).ToList();
            ViewBag.cl = numl.Count();
            //cantidad de tutoriales
            var numt = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(t => t.tutorial.idPublicacion).ToList();
            ViewBag.ct = numt.Count();
            //cantidad de cursos
            var numc = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(t => t.curso.idPublicacion).ToList();
            ViewBag.cc = numc.Count();
            return View();
        }
        public ActionResult Vtutorial(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vertutorial> dataA = db.tutorials.Where(d => d.idPublicacion == id).Select(d => new vertutorial()
            {
                idPublicacion = id,
                archivo = d.publicacion.archivos.ToList(),
                perf = d.publicacion.aspnet_User.perfils.ToList(),
                ava = d.publicacion.aspnet_User.avatars.OrderByDescending(i => i.idAvatar).ToList(),
                nombre = d.publicacion.aspnet_User.UserName,
                puntaje = d.publicacion.aspnet_User.karma.total,
                fecha = d.fecha,
                titulo = d.titulo,
                detalle = d.detalle
            }).ToList();
            ViewBag.tuto = dataA;
            ViewBag.id = id;
            //recuperamos el usuario q ha publicado para obtener sus 10 publicaciones-articulo
            System.Guid idus = db.publicacions.Where(g => g.idPublicacion == id).Select(f => f.UserId).ToArray()[0];
            List<mostrararticulos> lisusua = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.articulo.titulo,
            }).ToList();
            ViewBag.lua = lisusua;
            List<mostrartutoriales> lisusut = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.tutorial.titulo,
            }).ToList();
            ViewBag.lut = lisusut;
            List<mostrarcursos> lisusuc = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.curso.titulo,
            }).ToList();
            ViewBag.luc = lisusuc;
            List<mostrarlibros> lisusul = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarlibros()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.libro.titulo,
            }).ToList();
            ViewBag.lul = lisusul;

            //mostrar cant de comentario
            var cantcomen = db.comentarios.Where(i => i.idPublicacion == id).ToList().Count();
            ViewBag.cantidadc = cantcomen;
            //mostrar comentarios 
            List<vercomentario> todoscometarios = db.comentarios.Where(i => i.idPublicacion == id).Where(o => o.aspnet_User.UserId == o.UserId).Select(f => new vercomentario()
            {
                nick = f.aspnet_User.UserName,
                ava = f.aspnet_User.avatars.ToList(),
                Descripcion = f.Descripcion,
                fecha = f.fecha,
                puntaje = f.aspnet_User.karma.total
            }).ToList();
            ViewBag.todos = todoscometarios;
            //categoria
            List<Categoria> c = db.publicacion_categorias.Where(u => u.idPublicacion == id).Select(i => new Categoria() { nombre = i.categoria.nombre, idCategoria = i.idCategoria }).ToList();
            ViewBag.c = c;
            System.Guid idusu = db.publicacions.Where(i => i.idPublicacion == id).Select(a => a.aspnet_User.UserId).ToArray()[0];
            //cantidad de articulos
            var num = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(t => t.articulo.idPublicacion).ToList();
            ViewBag.ca = num.Count();
            //cantidad de libros
            var numl = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(t => t.libro.idPublicacion).ToList();
            ViewBag.cl = numl.Count();
            //cantidad de tutoriales
            var numt = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(t => t.tutorial.idPublicacion).ToList();
            ViewBag.ct = numt.Count();
            //cantidad de cursos
            var numc = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(t => t.curso.idPublicacion).ToList();
            ViewBag.cc = numc.Count();

            return View();
        }
        public ActionResult Vlibro(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<VerLibro> dataA = db.libros.Where(d => d.idPublicacion == id).Select(d => new VerLibro()
            {
                idPublicacion = id,
                archivo = d.publicacion.archivos.ToList(),
                perf = d.publicacion.aspnet_User.perfils.ToList(),
                ava = d.publicacion.aspnet_User.avatars.OrderByDescending(i=>i.idAvatar).ToList(),
                nombre = d.publicacion.aspnet_User.UserName,
                id = d.publicacion.aspnet_User.UserId,
                puntaje=d.publicacion.aspnet_User.karma.total,
                
                fecha = d.fecha,
                titulo = d.titulo,
                detalle = d.detalle,
                Annio_pub = (int)d.Annio_pub,
                Autor = d.Autor
             
            }).ToList();
            ViewBag.lib = dataA;
            ViewBag.id = id;
            //recuperamos el usuario q ha publicado para obtener sus 10 publicaciones-articulo
            System.Guid idus = db.publicacions.Where(g => g.idPublicacion == id).Select(f => f.UserId).ToArray()[0];
            List<mostrararticulos> lisusua = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.articulo.titulo,
            }).ToList();
            ViewBag.lua = lisusua;
            List<mostrartutoriales> lisusut = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.tutorial.titulo,
            }).ToList();
            ViewBag.lut = lisusut;
            List<mostrarcursos> lisusuc = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.curso.titulo,
            }).ToList();
            ViewBag.luc = lisusuc;
            List<mostrarlibros> lisusul = db.publicacions.Where(tt => tt.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarlibros()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.libro.titulo,
            }).ToList();
            ViewBag.lul = lisusul;

            //mostrar cant de comentario
            var cantcomen = db.comentarios.Where(i => i.idPublicacion == id).ToList().Count();
            ViewBag.cantidadc = cantcomen;
            //mostrar comentarios 
            List<vercomentario> todoscometarios = db.comentarios.Where(i => i.idPublicacion == id).Where(o => o.aspnet_User.UserId == o.UserId).Select(f => new vercomentario()
            {
                nick = f.aspnet_User.UserName,
                ava = f.aspnet_User.avatars.ToList(),
                Descripcion = f.Descripcion,
                fecha = f.fecha,
                puntaje = f.aspnet_User.karma.total
            }).ToList();
            ViewBag.todos = todoscometarios;
            List<Categoria> c = db.publicacion_categorias.Where(u => u.idPublicacion == id).Select(i => new Categoria() { nombre = i.categoria.nombre, idCategoria = i.idCategoria }).ToList();
            ViewBag.c = c;
            
            System.Guid idusu=db.publicacions.Where(i=>i.idPublicacion==id).Select(a=>a.aspnet_User.UserId).ToArray()[0];
            //cantidad de articulos
            var num = db.publicacions.Where(d=>d.UserId==idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(t => t.articulo.idPublicacion).ToList();
            ViewBag.ca=num.Count();
            //cantidad de libros
            var numl = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(t => t.libro.idPublicacion).ToList();
            ViewBag.cl = numl.Count();
            //cantidad de tutoriales
            var numt = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(t => t.tutorial.idPublicacion).ToList();
            ViewBag.ct = numt.Count();
            //cantidad de cursos
            var numc = db.publicacions.Where(d => d.UserId == idusu).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(t => t.curso.idPublicacion).ToList();
            ViewBag.cc = numc.Count();
            
           

            return View();
        }
      
        public ActionResult VCategoria(int idc) {
            DataClasses1DataContext db =new DataClasses1DataContext();
            List<Publicacion> cp = db.publicacion_categorias.Where(q => q.idCategoria == idc).Where(o => o.publicacion.estado == true).Select(x => new Publicacion() { idPublicacion = x.idPublicacion }).ToList();
            ViewBag.cp = cp;
            return View();
        }

        public ActionResult Comentar(int id) {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult Comentar(Comentario model, int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            if (Session["ids"] != null)
            {
                if (db.comentarios.Where(ss => ss.UserId == (Guid)Session["ids"]).Where(tt => tt.infraccion == 1).ToList().Count <= 3)
                {
                    System.Guid idusu = (Guid)Session["ids"];
                    string[] arraycomentario = model.Descripcion.ToLower().Split(' ');
                    List<diccionario> malaspalabras = new List<diccionario>();
                    foreach (var items in arraycomentario)
                    {
                        string item = items.Trim();
                        if (db.diccionarios.Where(x => x.palabra == item).Count() != 0)
                        {
                            foreach (var ii in db.diccionarios)
                            {
                                if (ii.palabra == item)
                                {
                                    malaspalabras.Add(new diccionario() { palabra = item });
                                }
                            }
                        }
                    }
                    if (malaspalabras.ToList().Count() > 0)
                    {

                        comentario a = new comentario() { UserId = idusu, idPublicacion = id, Descripcion = model.Descripcion, puntuacion = 0.5, fecha = DateTime.Now, infraccion = 1 };
                        db.comentarios.InsertOnSubmit(a);
                        db.SubmitChanges();
                    }
                    else
                    {
                        comentario a = new comentario() { UserId = idusu, idPublicacion = id, Descripcion = model.Descripcion, puntuacion = 0.5, fecha = DateTime.Now, infraccion = 0 };
                        db.comentarios.InsertOnSubmit(a);
                        db.SubmitChanges();
                    }
                    //actualiza su karma
                    if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
                    {
                        karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 0.5, fecha = DateTime.Now };
                        db.karmas.InsertOnSubmit(k);
                        db.SubmitChanges();
                    }
                    else
                    {
                        List<karma> karma = db.karmas.Where(g => g.UserId == idusu).ToList();
                        //kar.Sum = kar(double) + 10;
                        double j = 0;
                        foreach (var u in karma)
                        {
                            double h = u.total;
                            j = h + 0.5;
                        }
                        var l = db.karmas.Where(ll => ll.UserId == idusu);
                        karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                        l.ToArray()[0].UserId = idusu;
                        l.ToArray()[0].detalle = "puntaje";
                        l.ToArray()[0].total = j;
                        l.ToArray()[0].fecha = DateTime.Now;
                        db.SubmitChanges();

                    }
                    int i = id;
                    if (db.libros.Where(v => v.idPublicacion == i).ToList().Count != 0)
                    {
                        return RedirectToAction("Vlibro", "Home", new { id = id });
                    }
                    if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count != 0)
                    {
                        return RedirectToAction("Vcurso", "Home", new { id = id });
                    }
                    if (db.articulos.Where(v => v.idPublicacion == i).ToList().Count != 0)
                    {
                        return RedirectToAction("Varticulo", "Home", new { id = id });

                    }
                    if (db.tutorials.Where(v => v.idPublicacion == i).ToList().Count != 0)
                    {
                        return RedirectToAction("Vtutorial", "Home", new { id = id });

                    }

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    System.Guid IdUs = db.aspnet_Users.Where(a => a.UserId == (Guid)Session["ids"]).Select(a => a.UserId).ToArray()[0];
                    System.Guid IdRol = db.aspnet_Roles.Where(a => a.RoleName == "vaneado").Select(a => a.RoleId).ToArray()[0];
                    //elimina la fila relacion
                    aspnet_UsersInRole v = db.aspnet_UsersInRoles.Where(n => n.UserId == IdUs).ToArray()[0];
                    db.aspnet_UsersInRoles.DeleteOnSubmit(v);
                    db.SubmitChanges();

                    //agrega la nueva relacion de usuario vaneado
                    var vane = db.aspnet_UsersInRoles.Where(f => f.UserId == IdUs);
                    aspnet_UsersInRole rel = new aspnet_UsersInRole() { RoleId = IdRol, UserId = IdUs };
                    db.aspnet_UsersInRoles.InsertOnSubmit(rel);
                    db.SubmitChanges();


                    ViewBag.Message = "UD A SIDO BANEADO DEL SISTEMA POR REALIZAR COMENTARIOS ABUSIVOS";
                    return RedirectToAction("LogOn", "Account");
                }
            }
            else
            {
                ViewBag.Message = "DEBE ESTAR REGISTRADO";
                return RedirectToAction("LogOn", "Account");

            }
        }

        public ActionResult Libros() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //todos los libros
            List<mostrarlibros> listlibro = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.libro.idPublicacion).Select(a => new mostrarlibros()
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
        public ActionResult Articulos() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //todos los articulos
            List<mostrararticulos> listarti = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Select(a => new mostrararticulos()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.articulo.titulo,
                detalle = a.articulo.detalle
            }).ToList();
            ViewBag.listaarticulo = listarti;


            return View();
        }
        public ActionResult Cursos(){
            DataClasses1DataContext db = new DataClasses1DataContext();
            //10 ultimos cursos
            List<mostrarcursos> listcurso = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Select(a => new mostrarcursos()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.curso.titulo,
                detalle = a.curso.detalle.ToString().Substring(1, 2)
            }).ToList();
            ViewBag.listacurso = listcurso;
           
            return View();
        }
        public ActionResult Tutoriales() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //10 ultimos tutoriales
            List<mostrartutoriales> listtuto = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.tutorial.idPublicacion).Select(a => new mostrartutoriales()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.tutorial.titulo,
                detalle = a.tutorial.detalle.Substring(1, 5)
            }).ToList();
            ViewBag.listatutorial = listtuto;

            return View();
        }

        //ultimossssssssssssssssssssssssssssssssssssssssssssssssssss 10 

        public ActionResult Librosultimo()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //todos los libros
            List<mostrarlibros> listlibro = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).OrderByDescending(o => o.libro.idPublicacion).Take(10).Select(a => new mostrarlibros()
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
        public ActionResult Articulosultimo()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //todos los articulos
            List<mostrararticulos> listarti = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).OrderByDescending(o => o.articulo.idPublicacion).Take(10).Select(a => new mostrararticulos()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.articulo.titulo,
                detalle = a.articulo.detalle
            }).ToList();
            ViewBag.listaarticulo = listarti;


            return View();
        }
        public ActionResult Cursosultimo()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //10 ultimos cursos
            List<mostrarcursos> listcurso = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).OrderByDescending(o => o.curso.idPublicacion).Take(10).Select(a => new mostrarcursos()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.curso.titulo,
                detalle = a.curso.detalle.ToString().Substring(1, 2)
            }).ToList();
            ViewBag.listacurso = listcurso;

            return View();
        }
        public ActionResult Tutorialesultimo()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            //10 ultimos tutoriales
            List<mostrartutoriales> listtuto = db.publicacions.Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).OrderByDescending(o => o.tutorial.idPublicacion).Take(10).Select(a => new mostrartutoriales()
            {
                archivo = a.archivos.ToList(),
                categoria = a.publicacion_categorias.Where(d => d.categoria.estado == true).Select(w => new Categoria() { nombre = w.categoria.nombre }).ToList(),
                idPublicacion = a.idPublicacion,
                nombre = a.aspnet_User.UserName,
                titulo = a.tutorial.titulo,
                detalle = a.tutorial.detalle.Substring(1, 5)
            }).ToList();
            ViewBag.listatutorial = listtuto;

            return View();
        }
        public ActionResult megusta(int id) 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            if ((Guid)Session["ids"] != null)
            {
                if (db.karma2s.Where(g => g.UserId == (Guid)Session["ids"]).ToList().Count() == 0)
                {
                    ViewBag.id = id;
                    System.Guid idus = (Guid)Session["ids"];
                    if (db.karmas.Where(g => g.UserId == (Guid)Session["ids"]).ToList().Count() == 0)
                    {
                        karma kk = new karma() { UserId = idus, detalle = "puntajes de karma", total = 1, fecha = DateTime.Now };
                        db.karmas.InsertOnSubmit(kk);
                        db.SubmitChanges();
                        ViewBag.estado = 1;
                        karma2 k = new karma2() { UserId = idus, idPublicacion = id };
                        db.karma2s.InsertOnSubmit(k);
                        db.SubmitChanges();
                    }
                    else
                    {
                        List<karma> karma = db.karmas.Where(g => g.UserId == idus).ToList();
                        double j = 0;
                        foreach (var u in karma)
                        {
                            double h = u.total;
                            j = h + 1;
                        }
                        var l = db.karmas.Where(ll => ll.UserId == idus);
                        karma ka = new karma() { UserId = idus, detalle = "puntaje", total = j, fecha = DateTime.Now };
                        l.ToArray()[0].UserId = idus;
                        l.ToArray()[0].detalle = "puntaje";
                        l.ToArray()[0].total = j;
                        l.ToArray()[0].fecha = DateTime.Now;
                        db.SubmitChanges();
                        ViewBag.estado = 1;
                        karma2 k = new karma2() { UserId = idus, idPublicacion = id };
                        db.karma2s.InsertOnSubmit(k);
                        db.SubmitChanges();
                    }
                    int i = id;
                    if (db.libros.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vlibro", "Home", new { id = id });
                    }
                    if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vcurso", "Home", new { id = id });
                    }
                    if (db.articulos.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Varticulo", "Home", new { id = id });
                    }
                    if (db.tutorials.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vtutorial", "Home", new { id = id });
                    }
                }
                else
                {
                    ViewBag.estado = 0;
                    ViewBag.id = id;
                    System.Guid idus = (Guid)Session["ids"];
                    List<karma> karma = db.karmas.Where(g => g.UserId == idus).ToList();
                    double j = 0;
                    foreach (var u in karma)
                    {
                        double h = u.total;
                        j = h - 1;
                    }
                    var l = db.karmas.Where(ll => ll.UserId == idus);
                    karma k = new karma() { UserId = idus, detalle = "puntaje", total = j, fecha = DateTime.Now };
                    l.ToArray()[0].UserId = idus;
                    l.ToArray()[0].detalle = "puntaje";
                    l.ToArray()[0].total = j;
                    l.ToArray()[0].fecha = DateTime.Now;
                    db.SubmitChanges();
                    karma2 bk = db.karma2s.Where(r => r.UserId == idus).ToArray()[0];
                    db.karma2s.DeleteOnSubmit(bk);
                    db.SubmitChanges();

                    int i = id;
                    if (db.libros.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vlibro", "Home", new { id = id });
                    }
                    if (db.cursos.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vcurso", "Home", new { id = id });
                    }
                    if (db.articulos.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Varticulo", "Home", new { id = id });
                    }
                    if (db.tutorials.Where(v => v.idPublicacion == i).ToList().Count() != 0)
                    {
                        return RedirectToAction("Vtutorial", "Home", new { id = id });
                    }
                }
            }
            else 
            {
                return RedirectToAction("LogOn","Account");
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Descargar(int id)
        {
            if (Session["ids"] != null)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                var nombre = db.libros.Where(a => a.idPublicacion == id).Select(b => b.rutavirtual).ToArray()[0];
                var nombre1 = db.libros.Where(a => a.idPublicacion == id).Select(b => b.nombrelibro).ToArray()[0];
                var dir = Server.MapPath("../Libro");
                var path = Path.Combine(dir, nombre);
                return File(path, "application/pdf", nombre1);


            }
            else
            {
                ViewBag.Message = "debe estar registrado";
                return RedirectToAction("LogOn", "Account");
            }
        }
    }
} 
