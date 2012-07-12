using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    //[Authorize(Roles = "administrador")]
    public class ModeracionContenidoController : Controller
    {
        // GET: /ModeracionContenido/

        public ActionResult Index()
        {
            return View();
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
                ViewBag.Message="debe estar registrado";
                return RedirectToAction("LogOn", "Account");
            }
        }
        //ARTICULO
        public ActionResult VerArticulos() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listapendientearticulo> listar = db.publicacions.Where(a => a.estado == false).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(a => new listapendientearticulo()
            { 
                idPublicacion=a.idPublicacion,
                nombre=a.aspnet_User.UserName,
                titulo=a.articulo.titulo
            }).ToList();
            ViewBag.listado = listar;
            return View();
        }
        public ActionResult VerArticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<verarticulo> data = db.articulos.Where(d => d.idPublicacion == id).Select(d => new verarticulo() { 
                idPublicacion = id,
                nombre = d.publicacion.aspnet_User.UserName,
                titulo=d.titulo,
                detalle=d.detalle,
                fecha=d.fecha,
                archivo = d.publicacion.archivos.ToList()}).ToList();
              ViewBag.articulo = data;
            return View();
        }
        public ActionResult PublicarArticulo(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1


            System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
            if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
            {
                karma k = new karma() { UserId = idusu ,detalle = "puntaje  de karma", total = 10, fecha = DateTime.Now };
                db.karmas.InsertOnSubmit(k);
                db.SubmitChanges();
            }
            else {

                List<karma>   karma= db.karmas.Where(g => g.UserId == idusu).ToList();
                //kar.Sum = kar(double) + 10;
                double j = 0;
                foreach (var u in karma) {
                    double h = u.total ;
                     j = h + 10;
                }
                var l=db.karmas.Where(ll=>ll.UserId == idusu);
                karma ka = new karma() { UserId=idusu,detalle="puntaje",total = j, fecha=DateTime.Now };
                l.ToArray()[0].UserId = idusu;
                l.ToArray()[0].detalle ="puntaje";
                l.ToArray()[0].total = j;
                l.ToArray()[0].fecha = DateTime.Now;
                db.SubmitChanges();

            }
            
            //return View();
            return RedirectToAction("Index","ModeracionContenido");
        
        }
        //LIBRO
        public ActionResult VerLibros() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            
            //List<listapendientelibro> librospendientes = db.publicacions.Where(es => es.estado == false).Select(ess => new listapendientelibro() { idPublicacion = ess.idPublicacion, libro = db.libros.ToList(), nombre = db.publicacions.Where(u => u.idPublicacion == ess.idPublicacion).Select(y => y.aspnet_User.UserName).ToArray()[0] , rutavirtual=ar.rutavirtual}).ToList();
            List<listapendientelibro> librospendientes = db.publicacions.Where(es => es.estado == false).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(a => new listapendientelibro() {archivo = a.archivos.ToList(),idPublicacion=a.idPublicacion,titulo=a.libro.titulo,id=a.aspnet_User.UserId}).ToList();
            ViewBag.lista = librospendientes;
            return View();
        }
        public ActionResult VerLibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<VerLibro> verlibro = db.publicacions.Where(b => b.idPublicacion == id).Select(h => new VerLibro() { 
            idPublicacion = id,
            titulo = h.libro.titulo,
            Autor = h.libro.Autor,
            Annio_pub = (int)h.libro.Annio_pub,
            detalle = h.libro.detalle,
            fecha=h.libro.fecha,
            nombre = h.aspnet_User.UserName,
            archivo = h.archivos.ToList()}).ToList();
            ViewBag.verlibro = verlibro;
            return View();
        }
        public ActionResult PublicarLibro(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();

             System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
             if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
             {
                 karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 100, fecha = DateTime.Now };
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
                     j = h + 100;
                 }
                 var l = db.karmas.Where(ll => ll.UserId == idusu);
                 karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                 l.ToArray()[0].UserId = idusu;
                 l.ToArray()[0].detalle = "puntaje";
                 l.ToArray()[0].total = j;
                 l.ToArray()[0].fecha = DateTime.Now;
                 db.SubmitChanges();
             }
            return RedirectToAction("Index", "ModeracionContenido");
        }
        //CURSO
        public ActionResult VerCursos() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listapendientecurso> listar = db.publicacions.Where(a => a.estado == false).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(a => new listapendientecurso()
            { 
                idPublicacion=a.idPublicacion,
                nombre=a.aspnet_User.UserName,
                titulo=a.curso.titulo
            }).ToList();
            ViewBag.lista = listar;
            return View();
        }

        public ActionResult VerCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vercurso> data = db.cursos.Where(d => d.idPublicacion == id).Select(d => new vercurso() {
                nombre = d.publicacion.aspnet_User.UserName,
                idPublicacion=id,
                titulo = d.titulo,
                detalle = d.detalle,
                fecha = d.fecha,
                archivo = d.publicacion.archivos.ToList()     
            }).ToList();
            ViewBag.curso = data;
            return View();
        }
        public ActionResult PublicarCurso(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1
             System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
             if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
             {
                 karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 200, fecha = DateTime.Now };
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
                     j = h + 200;
                 }
                 var l = db.karmas.Where(ll => ll.UserId == idusu);
                 karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                 l.ToArray()[0].UserId = idusu;
                 l.ToArray()[0].detalle = "puntaje";
                 l.ToArray()[0].total = j;
                 l.ToArray()[0].fecha = DateTime.Now;
                 db.SubmitChanges();
             }
            //return View();
            return RedirectToAction("Index", "ModeracionContenido");
        }
       
        //TUTORIAL
        public ActionResult VerTutoriales() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<listapendientetutorial> listar = db.publicacions.Where(a => a.estado == false).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(a => new listapendientetutorial()
            {
                idPublicacion = a.idPublicacion,
                titulo = a.tutorial.titulo,
                nombre = a.aspnet_User.UserName
                
            }).ToList();
            ViewBag.lista = listar;
            return View();
        
        }
        public ActionResult Vertutorial(int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vertutorial> data = db.tutorials.Where(d => d.idPublicacion == id).Select(d => new vertutorial() {
                idPublicacion=id,
                nombre= d.publicacion.aspnet_User.UserName,
                titulo = d.titulo, 
                detalle = d.detalle, 
                archivo =d.publicacion.archivos.ToList()
            }).ToList();
            //Tutorial info = data.ToArray()[0];
            ViewBag.tutorial = data;
            return View();
        }
        public ActionResult PublicarTutorial(int id) {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { estado = true };
            pub.ToArray()[0].estado = true;
            db.SubmitChanges();
            //cambia el estado a 1
            System.Guid idusu  = db.publicacions.Where(v=>v.idPublicacion==id).Select(i=>i.UserId).ToArray()[0];
            if (db.karmas.Where(d => d.UserId == idusu).ToList().Count == 0)
            {
                karma k = new karma() { UserId = idusu, detalle = "puntaje  de karma", total = 50, fecha = DateTime.Now };
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
                    j = h + 50;
                }
                var l = db.karmas.Where(ll => ll.UserId == idusu);
                karma ka = new karma() { UserId = idusu, detalle = "puntaje", total = j, fecha = DateTime.Now };
                l.ToArray()[0].UserId = idusu;
                l.ToArray()[0].detalle = "puntaje";
                l.ToArray()[0].total = j;
                l.ToArray()[0].fecha = DateTime.Now;
                db.SubmitChanges();
            }
            return RedirectToAction("Index", "ModeracionContenido");
        
        }

        public ActionResult Rechazar(int id)
        {
            ViewBag.ve = id;
            return View();
        }
        [HttpPost]
        public ActionResult Rechazar(observarcion model, int id)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var pub = db.publicacions.Where(a => a.idPublicacion == id);
            publicacion p = new publicacion() { observaciones = model.observaciones };
            pub.ToArray()[0].observaciones = model.observaciones;
            db.SubmitChanges();
            return RedirectToAction("Index", "ModeracionContenido");
        }
        public ActionResult ReportesPublicados() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var p = db.publicacions.Where(a => a.estado == true).Where(b => b.idPublicacion == b.articulo.idPublicacion).Select(aa=>aa.articulo.titulo).ToList();
            ViewBag.p = p.Count();
            var q = db.publicacions.Where(a => a.estado == true).Where(b => b.idPublicacion == b.curso.idPublicacion).ToList();
            ViewBag.q = q.Count();
            var r = db.publicacions.Where(a => a.estado == true).Where(b => b.idPublicacion == b.libro.idPublicacion).ToList();
            ViewBag.r = r.Count();
            var s = db.publicacions.Where(a => a.estado == true).Where(b => b.idPublicacion == b.tutorial.idPublicacion).ToList();
            ViewBag.s = s.Count();
            return View();
        }
       
        
    }
}
