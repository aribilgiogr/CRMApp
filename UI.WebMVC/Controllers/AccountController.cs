using Core.Concretes.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.WebMVC.Controllers
{
    // Authorize: Sadece giriş yapılırsa bu denetleyiciye ait aksiyonlara izin verileceğini belirtir. Rol bağımsızdır.
    [Authorize]
    public class AccountController(UserManager<ApplicationUser> userManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Roles: Sadece belirtilen rollere ait hesaplar bu aksiyona erişebilir, geri kalanlar için burası yetkisiz erişim (401) hatası verecektir.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> People()
        {
            return View(await userManager.GetUsersInRoleAsync("SalesPerson"));
        }

        // AllowAnonymous: Anonim erişime izin verilir. Tün denetleyici erişime kapatıldığı için girş yapılmadan erişilmesini istediğimiz aksiyonlara bu özellik eklenir.
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
    }
}
