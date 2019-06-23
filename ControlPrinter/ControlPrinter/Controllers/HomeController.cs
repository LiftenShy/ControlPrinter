using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlPrinter.Models;
using ControlPrinter.Service.Abstract;

namespace ControlPrinter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
        private IImageService _imageService;

        public HomeController(IUserService userService, IStorageService storageService, IImageService imageService)
        {
            _userService = userService;
            _storageService = storageService;
            _imageService = imageService;
            _imageService.ReceiveImage();
        }

        public IActionResult Index()
        {
            var userViewModel = new UserViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var image = _storageService.LoadPicture();
                userViewModel.ProcessedImage = image.ProcessedImagePath;
                userViewModel.OriginalImageName = image.OriginalImagePath;
                userViewModel.DifferentBetweenImageName = image.ResultImagePath;

                return View(userViewModel);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
