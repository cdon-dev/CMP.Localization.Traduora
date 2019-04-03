using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IStringLocalizer _localizer;

        public HomeController(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.HelloWorld = "Hello World";

            ViewBag.Messages = new [] { _localizer["oneTerm"], _localizer["twoTerm"], _localizer["threeTerm"] };

            return View();
        }
    }
}