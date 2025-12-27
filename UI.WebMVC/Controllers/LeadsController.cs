using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (new string[] { ".csv", ".json", ".xlsx" }.Contains(ext))
                {
                    var result = await service.ImportLeadsAsync(file, ext);
                    if (result.Success)
                    {
                        return RedirectToAction("index", "leads");
                    }
                    ModelState.AddModelError(string.Empty, result.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Just .csv, .xlsx, .json allowed!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "File is empty!");
            }

            return View();
        }
    }
}
