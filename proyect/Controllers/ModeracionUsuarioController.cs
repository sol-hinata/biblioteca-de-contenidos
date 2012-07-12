using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyect.Models;

namespace proyect.Controllers
{
    public class ModeracionUsuarioController : Controller
    {
        //
        // GET: /ModeracionUsuario/

        public ActionResult Index()
        {
            
            
            return View();
        }
        public ActionResult ListarUsuario() {
            DataClasses1DataContext db = new DataClasses1DataContext();
            aspnet_Role r = db.aspnet_Roles.Where(e => e.RoleName == "usuario").ToArray()[0];
            List<VerUsuarios> usuarios = db.aspnet_UsersInRoles.Where(a => a.RoleId == r.RoleId).Select(u => new VerUsuarios()
            {
                idus = u.aspnet_User.UserId,
                nombre = u.aspnet_User.UserName,
                ava = u.aspnet_User.avatars.ToList(),
                r = u.aspnet_Role.RoleName
            }).ToList();
            ViewBag.usu = usuarios;
            return View();
        }
        public ActionResult listaBaneados()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            aspnet_Role r = db.aspnet_Roles.Where(e => e.RoleName == "vaneado").ToArray()[0];
            List<VerUsuarios> usuarios = db.aspnet_UsersInRoles.Where(a => a.RoleId == r.RoleId).Select(u => new VerUsuarios()
            {
                idus = u.aspnet_User.UserId,
                nombre = u.aspnet_User.UserName,
                ava = u.aspnet_User.avatars.ToList(),
                r = u.aspnet_Role.RoleName
            }).ToList();
            ViewBag.usu = usuarios;
            return View();
        }
        public ActionResult DetalleUsuario(string idn)
        {   DataClasses1DataContext db = new DataClasses1DataContext();
            ViewBag.idn = idn;
            System.Guid idus = db.aspnet_Users.Where(e => e.UserName == idn).Select(u=>u.UserId).ToArray()[0];
            List<Usuario> usu = db.aspnet_Users.Where(d => d.UserId == idus).Select(e => new Usuario()
            {   per=e.perfils.ToList(),
                ava = e.avatars.ToList(),
                puntaje = e.karma.total,
            }).ToList();
            ViewBag.us = usu;
            //cantidad de articulos
            var num = db.publicacions.Where(d => d.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.articulo.idPublicacion).Select(t => t.articulo.idPublicacion).ToList();
            ViewBag.ca = num.Count();
            //cantidad de libros
            var numl = db.publicacions.Where(d => d.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.libro.idPublicacion).Select(t => t.libro.idPublicacion).ToList();
            ViewBag.cl = numl.Count();
            //cantidad de tutoriales
            var numt = db.publicacions.Where(d => d.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.tutorial.idPublicacion).Select(t => t.tutorial.idPublicacion).ToList();
            ViewBag.ct = numt.Count();
            //cantidad de cursos
            var numc = db.publicacions.Where(d => d.UserId == idus).Where(es => es.estado == true).Where(a => a.idPublicacion == a.curso.idPublicacion).Select(t => t.curso.idPublicacion).ToList();
            ViewBag.cc = numc.Count();
            //cantidad de comentarios realizados
            var numcomen = db.comentarios.Where(g => g.UserId == idus).ToList();
            ViewBag.cantcom = numcomen.Count(); 
            return View();
        }

        public ActionResult BuscarUsuario() 
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            return View();
        }
        [HttpPost]
        public ActionResult BuscarUsuario(Buscar mod)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var model = db.aspnet_Users.Where(user => user.UserName == mod.buscado);
            if (model.ToList().Count == 0)
            {
                ViewBag.u = "no se encontro el usuario";
            }
            else
            {
                System.Guid idus = db.aspnet_Users.Where(r => r.UserName == mod.buscado).Select(j => j.UserId).ToArray()[0];
                ViewBag.nom = mod.buscado;
            }
            return View();
        }
        public ActionResult Habilitar(string idn)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            System.Guid IdUs = db.aspnet_Users.Where(a => a.UserName == idn).Select(a => a.UserId).ToArray()[0];
            System.Guid IdRol = db.aspnet_Roles.Where(a => a.RoleName == "usuario").Select(a => a.RoleId).ToArray()[0];
            //elimina la fila relacion
            aspnet_UsersInRole v = db.aspnet_UsersInRoles.Where(n => n.UserId == IdUs).ToArray()[0];
            db.aspnet_UsersInRoles.DeleteOnSubmit(v);
            db.SubmitChanges();

            //agrega la nueva relacion de usuario vaneado
            var vane = db.aspnet_UsersInRoles.Where(f => f.UserId == IdUs);
            aspnet_UsersInRole rel = new aspnet_UsersInRole() { RoleId = IdRol, UserId = IdUs };
            db.aspnet_UsersInRoles.InsertOnSubmit(rel);
            db.SubmitChanges();

            return RedirectToAction("listaBaneados", "ModeracionUsusario");

 
        }
        public ActionResult VanearUsuario(string idn)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            System.Guid IdUs = db.aspnet_Users.Where(a => a.UserName == idn).Select(a => a.UserId).ToArray()[0];            
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

            return RedirectToAction("DetalleUsuario", "Home");
        }
        public ActionResult Reporte()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            aspnet_Role r = db.aspnet_Roles.Where(e => e.RoleName == "usuario").ToArray()[0];
            List<VerUsuarios> usuarios = db.aspnet_UsersInRoles.Where(a => a.RoleId == r.RoleId).Select(u => new VerUsuarios()
            {
                idus = u.aspnet_User.UserId,
                nombre = u.aspnet_User.UserName,
                ava = u.aspnet_User.avatars.ToList(),
                r = u.aspnet_Role.RoleName,
                Email = u.aspnet_User.aspnet_Membership.Email,
                CreateDate = u.aspnet_User.aspnet_Membership.CreateDate,
                fechau = u.aspnet_User.LastActivityDate
            }).ToList();
            ViewBag.usu = usuarios;
            aspnet_Role v = db.aspnet_Roles.Where(e => e.RoleName == "vaneado").ToArray()[0];
            List<VerUsuarios> vaneados = db.aspnet_UsersInRoles.Where(a => a.RoleId == v.RoleId).Select(u => new VerUsuarios()
            {
                idus = u.aspnet_User.UserId,
                nombre = u.aspnet_User.UserName,
                ava = u.aspnet_User.avatars.ToList(),
                r = u.aspnet_Role.RoleName,
                Email = u.aspnet_User.aspnet_Membership.Email,
                CreateDate = u.aspnet_User.aspnet_Membership.CreateDate,
                fechau = u.aspnet_User.LastActivityDate
            }).ToList();
            ViewBag.usuv = vaneados;
            return View();
        }
    }
}
