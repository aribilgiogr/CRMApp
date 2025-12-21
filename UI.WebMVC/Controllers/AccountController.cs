using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI.WebMVC.Controllers
{
    // Authorize: Sadece giriş yapılırsa bu denetleyiciye ait aksiyonlara izin verileceğini belirtir. Rol bağımsızdır.
    [Authorize]
    public class AccountController(IAccountService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await service.GetDetailAsync(User));
        }

        public async Task<IActionResult> Logout()
        {
            await service.LogoutAsync();
            return RedirectToAction("login");
        }

        // Roles: Sadece belirtilen rollere ait hesaplar bu aksiyona erişebilir, geri kalanlar için burası yetkisiz erişim (401) hatası verecektir.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> People()
        {
            return View(await service.GetAllAsync());
        }

        // AllowAnonymous: Anonim erişime izin verilir. Tün denetleyici erişime kapatıldığı için girş yapılmadan erişilmesini istediğimiz aksiyonlara bu özellik eklenir.
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = "null")
        {

            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("index", "home")!; // returnUrl boşsa domain adrese (localhost vb) yönlendir.

            // Form alanlarından zorunlu olanların uygun formatta doldurulup doldurulmadığına bakarız.
            if (ModelState.IsValid)
            {
                var result = await service.LoginAsync(model);

                if (result.Success) return LocalRedirect(returnUrl);

                ModelState.AddModelError(string.Empty, result.Message);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin"), HttpPost, ValidateAntiForgeryToken]
        //[AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model, string? returnUrl = null, bool isAdmin = false)
        {
            returnUrl ??= Url.Action("index", "home")!; // returnUrl boşsa domain adrese (localhost vb) yönlendir.
            if (ModelState.IsValid)
            {
                var result = await service.RegisterAsync(model, isAdmin);

                if (result.Success) return LocalRedirect(returnUrl);

                foreach (var error in result.Message.Split('|'))
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }
    }
}
