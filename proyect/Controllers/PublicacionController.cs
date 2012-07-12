using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /Publicacion/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Enviado()
        {

            return View();
        }
     
        public ActionResult Articulo()
        {
            return View();
        }
       [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Articulo(HttpPostedFileBase artiFile, Articulo model)
        {
            if (ModelState.IsValid)
             {
                DataClasses1DataContext db = new DataClasses1DataContext();
                if (artiFile.ContentLength > 0)
                {   //inserta en publicacion
                    publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                    db.publicacions.InsertOnSubmit(p);
                    db.SubmitChanges();
                    publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                    //guarda la imagen
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../imagenesarti"), Path.GetFileName(artiFile.FileName));
                    artiFile.SaveAs(filePath);
                    //inserta en archivo
                    archivo img = new archivo() { rutafisica = filePath, rutavirtual = "/imagenesarti/" + artiFile.FileName, idPublicacion = ip.idPublicacion, fecha = DateTime.Now };
                    db.archivos.InsertOnSubmit(img);
                    db.SubmitChanges();

                    //inserta en articulo
                    articulo a = new articulo() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 10, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                    db.articulos.InsertOnSubmit(a);
                    db.SubmitChanges();
                    //para las categorias
                    string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                    List<categoria> listacategoria = new List<categoria>();
                    List<categoria> listaverdadera = new List<categoria>();
                    List<publicacion_categoria> rel = new List<publicacion_categoria>();

                    foreach (var items in arraycategorias)
                    {
                        string item = items.Trim();
                        if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                        {
                            listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                        }
                        else
                        {
                            categoria idcat = db.categorias.Where(z => z.nombre == item).ToArray()[0];
                            listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = item, estado = false, fecha = DateTime.Now });
                        }

                    }
                    if (listacategoria.ToList().Count() > 0)
                    {
                        db.categorias.InsertAllOnSubmit(listacategoria);
                        db.SubmitChanges();
                        @ViewBag.mensaje = "las categorias se crearon con exito";

                    }
                    foreach (var nue in listacategoria)
                    {
                        categoria idcat = db.categorias.Where(z => z.nombre == nue.nombre).ToArray()[0];
                        listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = nue.nombre, estado = false, fecha = DateTime.Now });
                    }
                    foreach (var rela in listaverdadera)
                    {
                        rel.Add(new publicacion_categoria() { idCategoria = rela.idCategoria, idPublicacion = ip.idPublicacion });

                    }
                    if (rel.ToList().Count() > 0)
                    {
                        db.publicacion_categorias.InsertAllOnSubmit(rel);
                        db.SubmitChanges();
                    }
                }
                    else
                    {
                        return View(model);
                    }
                    return RedirectToAction("Enviado", "Publicacion");
                }
            return View(model);
        }
        //DESDE AQUI PARA EL LIBRO
        public ActionResult Libro()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Libro(HttpPostedFileBase uploadFile,HttpPostedFileBase libroFile, Libro model)
        {
                DataClasses1DataContext db = new DataClasses1DataContext();
                if (uploadFile.ContentLength > 0 && libroFile.ContentLength > 0)
                {   //llena en publicacion   
                    publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                    db.publicacions.InsertOnSubmit(p);
                    db.SubmitChanges();
                    publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                    //guarda la imagen
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../ImagenesLib"), Path.GetFileName(uploadFile.FileName));
                    uploadFile.SaveAs(filePath);
                    //inserta en archivo
                    archivo img = new archivo() { rutafisica = filePath, rutavirtual = "/ImagenesLib/" + uploadFile.FileName, idPublicacion = ip.idPublicacion, fecha = DateTime.Now };
                    db.archivos.InsertOnSubmit(img);
                    db.SubmitChanges();
                    //guarda el libro pdf
                    string librofilePath = Path.Combine(HttpContext.Server.MapPath("../Libro"), Path.GetFileName(libroFile.FileName));
                    uploadFile.SaveAs(librofilePath);
                    //inserta en libro
                    libro lib = new libro() { nombrelibro=libroFile.FileName, idPublicacion = ip.idPublicacion, titulo = model.titulo, fecha = DateTime.Now, puntuacion = 100, Autor = model.Autor, Annio_pub = model.Annio_pub, rutafisica = librofilePath, rutavirtual = "/Libro/" + libroFile.FileName, detalle = model.detalle };
                    db.libros.InsertOnSubmit(lib);
                    db.SubmitChanges();

                    //para las categorias
                    string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                    List<categoria> listacategoria = new List<categoria>();
                    List<categoria> listaverdadera = new List<categoria>();
                    List<publicacion_categoria> rel = new List<publicacion_categoria>();

                    foreach (var items in arraycategorias)
                    {
                        string item = items.Trim();
                        if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                        {
                            listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                        }
                        else
                        {
                            categoria idcat = db.categorias.Where(z => z.nombre == item).ToArray()[0];
                            listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = item, estado = false, fecha = DateTime.Now });
                        }

                    }
                    if (listacategoria.ToList().Count() > 0)
                    {
                        db.categorias.InsertAllOnSubmit(listacategoria);
                        db.SubmitChanges();
                        @ViewBag.mensaje = "las categorias se crearon con exito";

                    }
                    foreach (var nue in listacategoria)
                    {
                        categoria idcat = db.categorias.Where(z => z.nombre == nue.nombre).ToArray()[0];
                        listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = nue.nombre, estado = false, fecha = DateTime.Now });
                    }
                    foreach (var rela in listaverdadera)
                    {
                        rel.Add(new publicacion_categoria() { idCategoria = rela.idCategoria, idPublicacion = ip.idPublicacion });

                    }
                    if (rel.ToList().Count() > 0)
                    {
                        db.publicacion_categorias.InsertAllOnSubmit(rel);
                        db.SubmitChanges();
                    }

                     }
                    else
                    {
                        return View(model);
                    }
                    return RedirectToAction("Enviado", "Publicacion");
            }
        //CURSOS
        public ActionResult Curso(){
        
        return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Curso(HttpPostedFileBase cursoFile, Curso model)
        {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                if (cursoFile.ContentLength > 0)
                {
                    //inserta en publicacion
                    publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                    db.publicacions.InsertOnSubmit(p);
                    db.SubmitChanges();
                    publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                    //guarda la imagen
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../imagenescurso"), Path.GetFileName(cursoFile.FileName));
                    cursoFile.SaveAs(filePath);
                    //inserta en archivo
                    archivo img = new archivo() { rutafisica = filePath, rutavirtual = "/imagenescurso/" + cursoFile.FileName, idPublicacion = ip.idPublicacion, fecha = DateTime.Now };
                    db.archivos.InsertOnSubmit(img);
                    db.SubmitChanges();
                    //inserta en curso
                    curso a = new curso() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 200, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                    db.cursos.InsertOnSubmit(a);
                    db.SubmitChanges();

                    //para las categorias
                    string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                    List<categoria> listacategoria = new List<categoria>();
                    List<categoria> listaverdadera = new List<categoria>();
                    List<publicacion_categoria> rel = new List<publicacion_categoria>();

                    foreach (var items in arraycategorias)
                    {
                        string item = items.Trim();
                        if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                        {
                            listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                        }
                        else
                        {
                            categoria idcat = db.categorias.Where(z => z.nombre == item).ToArray()[0];
                            listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = item, estado = false, fecha = DateTime.Now });
                        }

                    }
                    if (listacategoria.ToList().Count() > 0)
                    {
                        db.categorias.InsertAllOnSubmit(listacategoria);
                        db.SubmitChanges();
                        @ViewBag.mensaje = "las categorias se crearon con exito";

                    }
                    foreach (var nue in listacategoria)
                    {
                        categoria idcat = db.categorias.Where(z => z.nombre == nue.nombre).ToArray()[0];
                        listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = nue.nombre, estado = false, fecha = DateTime.Now });
                    }
                    foreach (var rela in listaverdadera)
                    {
                        rel.Add(new publicacion_categoria() { idCategoria = rela.idCategoria, idPublicacion = ip.idPublicacion });

                    }
                    if (rel.ToList().Count() > 0)
                    {
                        db.publicacion_categorias.InsertAllOnSubmit(rel);
                        db.SubmitChanges();
                    }
                }
                else
                {
                    return View(model);
                }
                return RedirectToAction("Enviado", "Publicacion");
            }
            return View(model);
        }

        //TUTORIALES 
        public ActionResult Tutorial() {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Tutorial(HttpPostedFileBase tutoFile, Tutorial model)
        {
            if (ModelState.IsValid)
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                if (tutoFile.ContentLength > 0)
                {
                    //inserta en publicacion
                    publicacion p = new publicacion() { UserId = (Guid)Session["ids"], estado = false, fecha = DateTime.Now };
                    db.publicacions.InsertOnSubmit(p);
                    db.SubmitChanges();
                    publicacion ip = db.publicacions.Where(b => b.UserId == (Guid)Session["ids"]).OrderByDescending(e => e.idPublicacion).ToArray()[0];
                    //guarda la imagen 
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../imagenescurso"), Path.GetFileName(tutoFile.FileName));
                    tutoFile.SaveAs(filePath);
                    //inserta en archivo
                    archivo img = new archivo() { rutafisica = filePath, rutavirtual = "/imagenescurso/" + tutoFile.FileName, idPublicacion = ip.idPublicacion, fecha = DateTime.Now };
                    db.archivos.InsertOnSubmit(img);
                    db.SubmitChanges();
                    //inserta en tutorial
                    tutorial a = new tutorial() { titulo = model.titulo, fecha = DateTime.Now, puntuacion = 50, detalle = model.detalle, idPublicacion = ip.idPublicacion };
                    db.tutorials.InsertOnSubmit(a);
                    db.SubmitChanges();

                    //para las categorias
                    string[] arraycategorias = model.nombrecate.ToLower().Split(',');
                    List<categoria> listacategoria = new List<categoria>();
                    List<categoria> listaverdadera = new List<categoria>();
                    List<publicacion_categoria> rel = new List<publicacion_categoria>();

                    foreach (var items in arraycategorias)
                    {
                        string item = items.Trim();
                        if (db.categorias.Where(b => b.nombre == item).Count() == 0)
                        {
                            listacategoria.Add(new categoria() { nombre = item, estado = false, fecha = DateTime.Now });

                        }
                        else
                        {
                            categoria idcat = db.categorias.Where(z => z.nombre == item).ToArray()[0];
                            listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = item, estado = false, fecha = DateTime.Now });
                        }

                    }
                    if (listacategoria.ToList().Count() > 0)
                    {
                        db.categorias.InsertAllOnSubmit(listacategoria);
                        db.SubmitChanges();
                        @ViewBag.mensaje = "las categorias se crearon con exito";

                    }
                    foreach (var nue in listacategoria)
                    {
                        categoria idcat = db.categorias.Where(z => z.nombre == nue.nombre).ToArray()[0];
                        listaverdadera.Add(new categoria() { idCategoria = idcat.idCategoria, nombre = nue.nombre, estado = false, fecha = DateTime.Now });
                    }
                    foreach (var rela in listaverdadera)
                    {
                        rel.Add(new publicacion_categoria() { idCategoria = rela.idCategoria, idPublicacion = ip.idPublicacion });

                    }
                    if (rel.ToList().Count() > 0)
                    {
                        db.publicacion_categorias.InsertAllOnSubmit(rel);
                        db.SubmitChanges();
                    }
                }
                else
                {
                    return View(model);
                }
                return RedirectToAction("Enviado", "Publicacion");
            }
            return View(model);
        }
    }
}
