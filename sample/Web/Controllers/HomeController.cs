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

        public IActionResult Index(string locale = "de_DE")
        {
            ViewBag.HelloWorld = "Hello World";

            ViewBag.SomeMessage = _localizer["first"];

            return View();
        }
    }
}