using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

using CP.Data;
using CP.Data.Models;
using CP.Storage;
using CP.Web.Models;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
#if DEBUG
        IFileManager _fileManager = new LocalFileManager(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Images/"));
#else
        IFileManager _fileManager = new CloudinaryFileManager();
#endif

        //[Authorize(Roles = "user,admin")]
        public ActionResult Index()
        {
            using (IRepository<Image> imageRepository = new EfRepository<Image>())
            {
                Image img = imageRepository.Table.OrderByDescending(o => o.DateLoad).FirstOrDefault();
                if (img != null)
                {
                    return this.View("Index",
                        new ImageModel
                        {
                            FileName = img.NameImage,
                            FilePath = this._fileManager.GetURI(img.NameImage)
                            //.Replace(this.Server.MapPath("~/"), "~/")
                        });
                }
            }
            return this.View("Index");
        }

        [HttpPost]
        public ActionResult IndexPost(HttpPostedFileBase fileBase)
        {
            string fileName = Guid.NewGuid().ToString();// + Path.GetExtension(fileBase.FileName);
            this._fileManager.SaveFile(fileName, fileBase.InputStream);
            using (IRepository<Image> imageRepository = new EfRepository<Image>())
            {
                imageRepository.Insert(new Image {NameImage = fileName, DateLoad = DateTime.Now});
            }
            return this.View("Index",
                new ImageModel
                {
                    FileName = fileName,
                    FilePath = this._fileManager.GetURI(fileName)//.Replace(this.Server.MapPath("~/"), "~/")
                });
        }
    }
}