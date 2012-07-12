using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;
namespace proyect.Controllers
{
    public class ModeracionComentarioController : Controller
    {
        //
        // GET: /ModeracionComentario/

        public ActionResult Index()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<vercomentario> listcomentarios = db.comentarios.Where(a => a.infraccion == 1).Select(f => new vercomentario()
            {
                idComentario = f.idComentario,
                nick = f.aspnet_User.UserName,
                Descripcion = f.Descripcion,
                fecha = f.fecha,
            }).ToList();
            ViewBag.lista = listcomentarios;

            return View();
        }
        public ActionResult BorrarComentario(int id) 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            comentario comentario = db.comentarios.Where(q => q.idComentario == id).ToArray()[0];
            db.comentarios.DeleteOnSubmit(comentario);
            db.SubmitChanges();
            return RedirectToAction("Index", "ModeracionComentario");

        }
    }
}
