using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface ILeadService {
        Task<IDataResult<IEnumerable<LeadListDTO>>> GetAllAsync(string? uid);
        Task<IResult> AddLeadAsync(LeadCreateDTO model);

        Task<IResult> ImportLeadsAsync(IFormFile file, string ext);

        Task<IResult> AssignLeadToSalesPersonAsync(string userId, int leadId);
    }
}
