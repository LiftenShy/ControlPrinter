using System.Web.Http;
using System.Web.Security;
using CP.Web.Models;

namespace CP.Web.Controllers.LoginAPI
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        public bool SignIn([FromBody]LoginModel model)
        {
            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(model.UserName, true);
                return true;
            }
            return false;
        }

        [HttpPut]
        [AllowAnonymous]
        public bool SignOut()
        {
            FormsAuthentication.SignOut();
            return true;
        }
    }
}