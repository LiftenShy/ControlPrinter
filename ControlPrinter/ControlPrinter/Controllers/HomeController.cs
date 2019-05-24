using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlPrinter.Models;
using ControlPrinter.Service.Abstract;

namespace ControlPrinter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var userViewModel = new UserViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var userModel = _userService.GetByName(User.Identity.Name);
                userViewModel.ProcessedImage = userModel.ProcessedImage;
                userViewModel.OriginalImageName = userModel.OriginalImageName;
                userViewModel.DifferentBetweenImageName = userModel.DifferentBetweenImageName;

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
