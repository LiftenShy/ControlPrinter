using System.Web.Mvc;
using System.Web.Security;
using CP.Business;
using CP.Data.Models;
using CP.Web.Models;
using Microsoft.Practices.Unity;

namespace CP.Web.Controllers.Account
{
    public class LoginController : Controller
    {
        [Dependency]
        public IUserService UserService { get; set; }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginPost(LoginModel model)
        {
            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(model.UserName, true);
                return this.RedirectToRoute(new {controller = "Home", action = "HomePage"});
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Enter your name or password incorrect");
                return this.View("Login");
            }
        }

        public ActionResult Registration()
        {
            return this.View();
        }

        public ActionResult RegistrationPost(LoginModel user)
        {
            if (UserService.AddUser(new User {UserName = user.UserName, Password = user.Password, RoleId = 1}))
            {
                return RedirectToAction("HomePage","Home");
            }
            ModelState.AddModelError(string.Empty, "This name already use, choose another name");
            return View("Registration");
        }

        public void LoginOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.UserService != null)
            {
                this.UserService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}