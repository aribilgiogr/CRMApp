using Core.Concretes.DTOs;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface IActivityService {
        Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByLeadId(int leadId);
        Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByCustomerId(int customerId);
        Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByOpportunityId(int opportunityId);
        Task<IResult> AddActivityToLeadAsync(ActivityCreateDTO model);
        Task<IResult> AddActivityToOpportunityAsync(ActivityCreateDTO model);
        Task<IResult> AddActivityToCustomerAsync(ActivityCreateDTO model);
    }
}
