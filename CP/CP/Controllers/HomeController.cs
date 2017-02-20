using System;
using System.Web;
using System.Web.Mvc;
using CP.Business;
using CP.Data.Models;
using CP.Web.Models;
using Microsoft.Practices.Unity;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IImageService ImageService { get; set; }

        [Authorize(Roles = "user,admin")]
        public ActionResult HomePage()
        {
            Image img = this.ImageService.GetImage();
            return this.View("HomePage",new ImageModel {FileName = img.NameImage,
                                                     FilePath = this.ImageService.GetImageUri(img.NameImage)});
        }

        [HttpPost]
        public ActionResult HomePagePost(HttpPostedFileBase fileBase)
        {
            Image image = new Image {NameImage = fileBase.FileName, DateLoad = DateTime.Now};
            this.ImageService.SaveImage(image,fileBase.InputStream);
            return this.RedirectToAction("HomePage"); //this.View("HomePage", new ImageModel {FileName = image.NameImage,FilePath = this.ImageService.GetImageUri(image.NameImage)});
        } 

        protected override void Dispose(bool disposing)
        {
            if (this.ImageService != null)
            {
                this.ImageService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}