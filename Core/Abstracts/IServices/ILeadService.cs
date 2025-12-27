using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Http;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface ILeadService {
        Task<IDataResult<IEnumerable<LeadListDTO>>> GetAllAsync(string? uid);
        Task<IResult> AddLeadAsync(LeadCreateDTO model);

        Task<IResult> ImportLeadsAsync(IFormFile file, string ext);
    }
}
