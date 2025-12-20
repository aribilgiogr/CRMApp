using Microsoft.AspNetCore.Mvc;

namespace UI.WebMVC.Controllers
{
    public class SalesPeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
