using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SportsPro.Controllers
{

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

    [Route("About")] //Add Route
        public ViewResult About()
        {
            return View();
        }
    }
}