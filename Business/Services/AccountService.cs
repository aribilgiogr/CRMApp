using AutoMapper;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Utilities.Results;

namespace Business.Services
{
    public class AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : IAccountService
    {
        public Task<IDataResult<IEnumerable<UserListDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<UserDetailDTO>> GetDetailAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<UserDetailDTO>> GetDetailAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> LoginAsync(LoginDTO model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new SuccessResult();
            }

            if (result.IsLockedOut)
            {
                return new ErrorResult("Your account is locket out. Please try again later.");
            }

            if (result.IsNotAllowed)
            {
                return new ErrorResult("You must confirm your email address before logging in.");
            }

            return new ErrorResult("Invalid login attempt!");
        }

        public async Task<IResult> LogoutAsync()
        {
            try
            {
                await signInManager.SignOutAsync();
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult("Logout Exception: " + ex.Message);
            }
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model, bool isAdmin = false)
        {
            var user = mapper.Map<ApplicationUser>(model);
            user.UserName = model.Email;
            user.IsAdmin = isAdmin;

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.Roles.AnyAsync(r => r.Name == "Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                }

                if (!await roleManager.Roles.AnyAsync(r => r.Name == "SalesPerson"))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "SalesPerson" });
                }

                //Aktivasyon maili vb yapılar burada çalışacak.
                await userManager.AddToRoleAsync(user, isAdmin ? "Admin" : "SalesPerson");
                return new SuccessResult();
            }

            return new ErrorResult(string.Join("|", result.Errors.Select(e => e.Description)));
        }
    }
}
