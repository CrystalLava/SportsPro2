using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Privacy()
        {
            return View();
        }

          

    [Route("About")] //Add Route
        public ViewResult About()
        {
            return View();
        }
    }
}