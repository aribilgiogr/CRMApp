using Core.Concretes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface ILeadService {
        Task<IDataResult<IEnumerable<LeadListDTO>>> GetAllAsync(string? uid);
        Task<IResult> AddLeadAsync(LeadCreateDTO model);
    }
}
