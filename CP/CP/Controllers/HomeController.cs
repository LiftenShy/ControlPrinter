using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using CP.Data;
using CP.Data.Models;
using  CP.Storage;
using CP.Web.Models;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize(Roles = "user,admin")]
        public ActionResult Index()
        {
            return this.View();
        }


        public ActionResult IndexPost(HttpPostedFileBase fileBase)
        {
            using (IRepository<Data.Models.Image> imageRepository = new EfRepository<Data.Models.Image>())
            {
                imageRepository.Insert(new Data.Models.Image {NameImage = fileBase.FileName, DateLoad = DateTime.Now} );
            }
                /*IFileManager fileManager = new LocalFileManager(this.Server.MapPath(Path.Combine("~/Images/")));
                fileManager.SaveFile(Guid.NewGuid().ToString(), fileBase.InputStream);*/
                return this.View("Index");
        }
    }
}