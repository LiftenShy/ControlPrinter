using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult IndexPost(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(this.Server.MapPath("~/Images/"), pic);
                file.SaveAs(path);
                this.ViewData["Image"] = "~/Images/" + pic;
            }
            return this.View("Index");
        }
    }
}