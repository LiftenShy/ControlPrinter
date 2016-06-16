using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using CP.Data;
using CP.Data.Models;
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

        public ActionResult Registration()
        {
            return this.View();
        }

        public ActionResult RegistrationPost(LoginModel user)
        {
            using (IRepository<User> userRepository = new EfRepository<User>())
            {
                if (!userRepository.Table.Any(u => u.UserName == user.UserName))
                {
                    User item = new User {UserName = user.UserName, Password = user.Password, RoleId = 1};
                    userRepository.Insert(item);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "This name already use, choose another name");
                }
                return this.View("Login");
            }
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Login");
        }
    }
}