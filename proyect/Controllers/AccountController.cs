using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using proyect.Models;
using BotDetect.Web.UI.Mvc;

namespace proyect.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            // when the View is accessed directly and not posted, we 
            // clear any remembered CAPTCHA solving state. 
            // The users only have to solve the CAPTCHA once within a 
            // single registration, but if they reload the Register 
            // page later, it is shown again. 
            // Otherwise, they could register an unlimited number of 
            // accounts within a single Session after solving the 
            // CAPTCHA only once.


            MvcCaptcha.ResetCaptcha("RegistrationCaptcha");

            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegistrationCaptcha", "Your input doesn't match displayed characters.")]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //MvcCaptcha cad = CaptchaHelper.GetRegistrationCaptcha();
                if (Session["captcha"] != null && (int)Session["captcha"] >= 2 && MvcCaptcha.Validate("CaptchaCode", "RegistrationCaptcha", "Your input doesn't match displayed characters."))
                {
                    return View(model);
                }
                MembershipUser user = null;
                String userName;
                if (model.UserName.Contains("@"))
                {
                    userName = Membership.GetUserNameByEmail(model.UserName);
                    if (!string.IsNullOrEmpty(userName))
                    {
                        user = Membership.GetUser(userName);
                    }

                }
                else {
                    user = Membership.GetUser(model.UserName);
                    userName = model.UserName;

                }
                if (user!=null&&Membership.ValidateUser(userName, model.Password))
                {
                    
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        DataClasses1DataContext db = new DataClasses1DataContext();
                        aspnet_User data = db.aspnet_Users.Where(a => a.UserName == model.UserName).ToArray()[0];
                        if (Session["ids"] != null)
                        {
                            Session["ids"] = data.UserId;
                        }
                        
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        DataClasses1DataContext db = new DataClasses1DataContext();
                        aspnet_User data = db.aspnet_Users.Where(a => a.UserName == userName).ToArray()[0];
                        Session["ids"] = data.UserId;
                        System.Guid IdRol = db.aspnet_UsersInRoles.Where(a => a.UserId == (Guid)Session["ids"]).Select(a => a.RoleId).ToArray()[0];
                        aspnet_Role rool = db.aspnet_Roles.Where(c => c.RoleId == IdRol).ToArray()[0];
                        //ARREGLAR
                        Session["rol"] = rool.RoleName;
                        return RedirectToAction("Index", "Home");
                                    
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                if (Session["captcha"] == null)
                {
                    Session["captcha"] = 0;

                }
                else
                {
                    int a = (int)Session["captcha"];
                    a++;
                    Session["captcha"] = a;
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    DataClasses1DataContext db = new DataClasses1DataContext();
                    System.Guid IdUs = db.aspnet_Users.Where(a => a.UserName == model.UserName).Select(a => a.UserId).ToArray()[0];
                    System.Guid IdRol = db.aspnet_Roles.Where(a => a.RoleName == "Usuario").Select(a => a.RoleId).ToArray()[0];
                    aspnet_UsersInRole rel = new aspnet_UsersInRole() { RoleId = IdRol, UserId = IdUs };
                    db.aspnet_UsersInRoles.InsertOnSubmit(rel);
                    db.SubmitChanges();
                    

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        public string RoleName { get; set; }
    }
}
