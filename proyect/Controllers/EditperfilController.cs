using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
using System.IO;

namespace proyect.Controllers
{
    public class EditperfilController : Controller
    {
        //
        // GET: /Editperfil/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Perfil()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
                if (db.perfils.Where(m => m.UserId == (Guid)Session["ids"]).ToList().Count != 0)//si hay perfil
                {    //seleccionar el perfil
                    perfil iper = db.perfils.Where(e => e.UserId == (Guid)Session["ids"]).ToArray()[0];
                    if (db.avatars.Where(j => j.idPerfil == iper.idPerfil).ToList().Count != 0)
                    {//si hay  avatar
                        avatar av = db.avatars.Where(g => g.idPerfil == iper.idPerfil).OrderByDescending(ee => ee.idAvatar).ToArray()[0];
                        ViewBag.listaimg = av;
                        //seleccion su perfil
                        System.Guid ddd = (Guid)Session["ids"];
                        if (db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 1)
                        {
                            List<PerfilEdit> data = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).Select(d => new PerfilEdit() { nombre = d.nombre, apellido = d.apellido, fecha = d.fecha, apellidom = d.apellidom, interes = d.intereses, idPerfil = d.idPerfil, ubicacion = d.ubicacion }).ToList();

                            PerfilEdit info = data.ToArray()[0];
                            return View(info);
                        }
                    }
                    else
                    {//si no hay avatar
                        ViewBag.listaimg = null;
                        System.Guid ddd = (Guid)Session["ids"];
                        if (db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 1)
                        {
                            List<PerfilEdit> data = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).Select(d => new PerfilEdit() { nombre = d.nombre, apellido = d.apellido, fecha = d.fecha, apellidom = d.apellidom, interes = d.intereses, idPerfil = d.idPerfil, ubicacion = d.ubicacion }).ToList();

                            PerfilEdit info = data.ToArray()[0];
                            return View(info);
                        }
                    }
                }
           
            return View();
        }

        [HttpPost]
        public ActionResult Perfil(PerfilEdit model)
        {
            DataClasses1DataContext per = new DataClasses1DataContext();
            if (per.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 0)
            {
                string sqlTimeAsString = model.fecha.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                System.Guid IdUs = (Guid)Session["ids"];
                perfil p = new perfil() { nombre = model.nombre, apellido = model.apellido, apellidom = model.apellidom, ubicacion = model.ubicacion, intereses = model.interes, fecha = DateTime.Now, UserId = IdUs, };
                per.perfils.InsertOnSubmit(p);
                per.SubmitChanges();
                perfil idper = per.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToArray()[0];
                //llena por defecto al avatar si no tiene avatar
                if (per.avatars.Where(f => f.UserId == (Guid)Session["ids"]).ToList().Count == 0)
                {
                    avatar img = new avatar() { rutafisica = "C:/Users/Owner/Documents/Visual Studio 2010/Projects/proyect/proyect/Imagenes/noavatar.jpg", rutavirtual = "/Imagenes/noavatar.jpg", idPerfil = idper.idPerfil, UserId = (Guid)Session["ids"], fecha = DateTime.Now };
                    per.avatars.InsertOnSubmit(img);
                    per.SubmitChanges();
                }

                return RedirectToAction("Perfil", "EditPerfil");

            }
            else
            {
                var o = per.perfils.Where(a => a.UserId == (Guid)Session["ids"]);

                perfil p = new perfil() { nombre = model.nombre, apellido = model.apellido, apellidom = model.apellidom, ubicacion = model.ubicacion, intereses = model.interes, fecha = DateTime.Now, UserId = (Guid)Session["ids"] };
                o.ToArray()[0].nombre = model.nombre;
                o.ToArray()[0].apellido = model.apellido;
                o.ToArray()[0].apellidom = model.apellidom;
                o.ToArray()[0].intereses = model.interes;
                o.ToArray()[0].fecha = DateTime.Now;
                o.ToArray()[0].ubicacion = model.ubicacion;

                per.SubmitChanges();

                return RedirectToAction("Perfil", "EditPerfil");   
            }
            
        }
        public ActionResult SubirImg() {
           
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubirImg(HttpPostedFileBase uploadFile)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();


            if (db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToList().Count == 0)//si no tiene perfil
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Imagenes"), Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
                perfil p = new perfil() { nombre = "", apellido = "", apellidom = "", ubicacion = "", intereses = "", fecha = DateTime.Now, UserId = (Guid)Session["ids"] };
                db.perfils.InsertOnSubmit(p);
                db.SubmitChanges();
                perfil idper = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToArray()[0];
                avatar img = new avatar() { rutafisica = filePath, rutavirtual = "/Imagenes/" + uploadFile.FileName, idPerfil = idper.idPerfil, UserId = (Guid)Session["ids"], fecha = DateTime.Now };
                db.avatars.InsertOnSubmit(img);
                db.SubmitChanges();
                return RedirectToAction("Perfil", "EditPerfil");
            }
            else
            {
                if (uploadFile.ContentLength > 0)
                {
                    //AUMENTAdo
                    if (db.avatars.Where(r => r.UserId == (Guid)Session["ids"]).ToList().Count != 0)
                    {
                        string filePath = Path.Combine(HttpContext.Server.MapPath("../Imagenes"), Path.GetFileName(uploadFile.FileName));
                        uploadFile.SaveAs(filePath);
                        perfil idper = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToArray()[0];
                        var avas = db.avatars.Where(g => g.UserId == (Guid)Session["ids"]);
                        avatar av = new avatar() { rutafisica = filePath, rutavirtual = "/Imagenes/" + uploadFile.FileName, idPerfil = idper.idPerfil, UserId = (Guid)Session["ids"], fecha = DateTime.Now };
                        avas.ToArray()[0].rutafisica = filePath;
                        avas.ToArray()[0].rutavirtual = "/Imagenes/" + uploadFile.FileName;
                        avas.ToArray()[0].idPerfil = idper.idPerfil;
                        avas.ToArray()[0].UserId = (Guid)Session["ids"];
                        avas.ToArray()[0].fecha = DateTime.Now;
                        db.SubmitChanges();
                    }
                    else
                    {
                        string filePath = Path.Combine(HttpContext.Server.MapPath("../Imagenes"), Path.GetFileName(uploadFile.FileName));
                        uploadFile.SaveAs(filePath);
                        perfil idper = db.perfils.Where(a => a.UserId == (Guid)Session["ids"]).ToArray()[0];
                        avatar img = new avatar() { rutafisica = filePath, rutavirtual = "/Imagenes/" + uploadFile.FileName, idPerfil = idper.idPerfil, UserId = (Guid)Session["ids"], fecha = DateTime.Now };
                        db.avatars.InsertOnSubmit(img);
                        db.SubmitChanges();
                    }

                }
                return RedirectToAction("Perfil", "EditPerfil");
            }
       }   
    }
}
