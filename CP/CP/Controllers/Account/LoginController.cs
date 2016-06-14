using System.Web.Mvc;
using System.Web.Security;
using CP.Web.Models;

namespace CP.Web.Controllers.Account
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return this.View();
        }

        public ActionResult LoginPost(LoginModel model)
        {
            if(Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(model.UserName, true);
                return this.RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                return this.View("Login");
            }
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return this.View("Login");
        }
    }
}