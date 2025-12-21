using Core.Concretes.DTOs;
using System.Security.Claims;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface IAccountService
    {
        Task<IResult> LoginAsync(LoginDTO model);
        Task<IResult> LogoutAsync();
        Task<IResult> RegisterAsync(RegisterDTO model, bool isAdmin = false);
        Task<IDataResult<UserDetailDTO>> GetDetailAsync(ClaimsPrincipal principal); // Kullanıcının kendisi için
        Task<IDataResult<UserDetailDTO>> GetDetailAsync(string username); // Admin için
        Task<IDataResult<IEnumerable<UserListDTO>>> GetAllAsync();
    }
}
