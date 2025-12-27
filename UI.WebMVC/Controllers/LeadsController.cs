using Core.Abstracts.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.WebMVC.Controllers
{
    [Authorize]
    public class LeadsController(ILeadService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await service.GetAllAsync();
            if (model.Success)
            {
                return View(model.Data);
            }
            else
            {
                return Problem(model.Message);
            }
        }

        public IActionResult Create() { 
            return View();
        }
    }
}
