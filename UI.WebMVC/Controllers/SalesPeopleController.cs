using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.WebMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SalesPeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
