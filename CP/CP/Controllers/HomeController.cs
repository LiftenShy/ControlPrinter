using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CP.Business.Abstract;
using CP.Data.Models;
using CP.Data.ModelTransport;
using CP.Web.Models;
using Unity;

namespace CP.Web.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IImageService ImageService { get; set; }

        [Dependency]
        public IQueueService QueueService { get; set; }

        [Authorize(Roles = "user,admin")]
        public ActionResult HomePage()
        {
            return View("HomePage", new ImageModel
            {
                ImgUri = ImageService.GetImageUri(ImageService.GetImage().NameImage),
                ResultImgUri = ImageService.GetImageUri(ImageService.GetResultImage().NameImage),
                StaticImgUri = ImageService.GetImageUri(ImageService.GetStaticImage().NameImage)
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task HomePagePost(HttpPostedFileBase fileBase)
        {
            TransportImageModel trImgModel = new TransportImageModel {ImageBytes =new byte[fileBase.ContentLength]};
            fileBase.InputStream.Read(trImgModel.ImageBytes,0,fileBase.ContentLength);
            await QueueService.Send(new Uri("rabbitmq://localhost/mq"),trImgModel);
        }

        [HttpPost]
        public ActionResult StaticImg(HttpPostedFileBase fileBase)
        {
            StaticImage image = new StaticImage { NameImage = fileBase.FileName, DateLoad = DateTime.UtcNow };
            ImageService.SaveStatisticImage(image, fileBase.InputStream);
            return RedirectToAction("HomePage");
        }

        protected override void Dispose(bool disposing)
        {
            ImageService?.Dispose();
            base.Dispose(disposing);
        }
    }
}