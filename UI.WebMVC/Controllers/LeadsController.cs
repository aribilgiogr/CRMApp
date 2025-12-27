using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace UI.WebMVC.Controllers
{
    [Authorize]
    public class LeadsController(ILeadService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await service.GetAllAsync(User.IsInRole("Admin") ? null : User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (model.Success)
            {
                return View(model.Data);
            }
            else
            {
                return Problem(model.Message);
            }
        }

        public IActionResult Create()
        {
            ViewBag.Sources = new SelectList(Enum.GetNames<LeadSource>());
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeadCreateDTO model)
        {
            ViewBag.Sources = new SelectList(Enum.GetNames<LeadSource>(), model.Source);
            if (ModelState.IsValid)
            {
                var result = await service.AddLeadAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("index", "leads");
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }
    }
}
