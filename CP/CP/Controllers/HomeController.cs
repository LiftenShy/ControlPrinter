using System;
using System.Web;
using System.Web.Mvc;
using CP.Business;
using CP.Data.Models;
using CP.Web.Models;
using Emgu.CV;
using Emgu.CV.Structure;
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
            /*Image img = this.ImageService.GetImage();
            return this.View("HomePage",new ImageModel {FileName = img.NameImage,
                                                     FilePath = this.ImageService.GetImageUri(img.NameImage)});*/

            Asd();
            return View("HomePage",new ImageModel {Images = Asd()});
        }

        [HttpPost]
        public ActionResult HomePagePost(HttpPostedFileBase fileBase)
        {
            Image image = new Image {NameImage = fileBase.FileName, DateLoad = DateTime.Now};
            this.ImageService.SaveImage(image,fileBase.InputStream);
            return this.View("HomePage",
                new ImageModel {FileName = image.NameImage, FilePath = this.ImageService.GetImageUri(image.NameImage)});
        }

        public Image<Gray, Byte>[] Asd()
        {
            Image<Gray, Byte> img1 = new Image<Gray, Byte>(100, 50, new Gray(30));
            Image<Gray, Byte> img2 = img1.Not();
            Image<Gray, Byte> img3 = img1.Convert<Byte>(delegate (Byte b) { return (Byte)(255 - b); });
            Image<Gray, Byte>[] images = new Image<Gray, Byte>[3];
            images[0] = img1;
            images[1] = img2;
            images[2] = img3;
            return images;
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