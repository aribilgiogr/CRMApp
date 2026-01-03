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
    public class LeadsController(ILeadService service, IActivityService activityService, ICustomerService customerService) : Controller
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

        public async Task<IActionResult> PickLead(int id)
        {
            try
            {
                var result = await service.AssignLeadToSalesPersonAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), id);
                if (result.Success)
                {
                    return RedirectToAction("index", "leads");
                }
                else
                {
                    return Problem(result.Message);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivity(ActivityCreateDTO model)
        {
            Utilities.Results.IResult? result = null;
            string? uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (uid != null)
            {
                model.AssignedSalesPersonId = uid;
                ModelState.Remove("AssignedSalesPersonId"); // Kontrol edilmesini istemediğimiz alanları kaldırabiliriz.
                if (ModelState.IsValid)
                {
                    result = await activityService.AddActivityToLeadAsync(model);
                }
            }
            return RedirectToAction("index", "leads", new { result = result?.Success });
        }

        public async Task<IActionResult> ConvertToCustomer(int leadId)
        {
            var result = await service.GetAsync(leadId);
            if (result.Success)
            {
                var lead = result.Data;
                if (lead != null)
                {
                    if (lead.AssignedSalesPersonId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    {
                        var model = new CreateCustomerDTO
                        {
                            CompanyName = lead.CompanyName ?? lead.FullName,
                            AssignedSalesPersonId = lead.AssignedSalesPersonId,
                            Email = lead.Email,
                            Phone = lead.Phone,
                            LeadId = leadId
                        };
                        return View(model);
                    }
                }
            }
            return NotFound(result.Message);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConvertToCustomer(CreateCustomerDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await customerService.AddAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("index", "customers");
                }
                ModelState.AddModelError(string.Empty, result.Message);
            }
            return View(model);
        }
    }
}
