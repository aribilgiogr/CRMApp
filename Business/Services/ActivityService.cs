using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Utilities.Constants;
using Utilities.Results;

namespace Business.Services
{
    public class ActivityService(IUnitOfWork unitOfWork, IMapper mapper) : IActivityService
    {
        public async Task<IResult> AddActivityToCustomerAsync(ActivityCreateDTO model)
        {
            try
            {
                var activity = mapper.Map<Activity>(model);
                activity.RelatedCustomerId = model.RelatedId;
                await unitOfWork.ActivityRepository.CreateAsync(activity);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Activity" + Messages.AddedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }

        public async Task<IResult> AddActivityToLeadAsync(ActivityCreateDTO model)
        {
            try
            {
                var activity = mapper.Map<Activity>(model);
                activity.RelatedLeadId = model.RelatedId;
                await unitOfWork.ActivityRepository.CreateAsync(activity);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Activity" + Messages.AddedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }

        public async Task<IResult> AddActivityToOpportunityAsync(ActivityCreateDTO model)
        {
            try
            {
                var activity = mapper.Map<Activity>(model);
                activity.RelatedOpportunityId = model.RelatedId;
                await unitOfWork.ActivityRepository.CreateAsync(activity);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Activity" + Messages.AddedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByCustomerId(int customerId)
        {
            try
            {
                var activities = await unitOfWork.ActivityRepository.FindManyAsync(x => x.RelatedCustomerId == customerId);
                var activityDTOs = mapper.Map<IEnumerable<ActivityListDTO>>(activities);
                return new SuccessDataResult<IEnumerable<ActivityListDTO>>(activityDTOs, Messages.SuccessfulOperation);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<ActivityListDTO>>(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByLeadId(int leadId)
        {
            try
            {
                var activities = await unitOfWork.ActivityRepository.FindManyAsync(x => x.RelatedLeadId == leadId);
                var activityDTOs = mapper.Map<IEnumerable<ActivityListDTO>>(activities);
                return new SuccessDataResult<IEnumerable<ActivityListDTO>>(activityDTOs, Messages.SuccessfulOperation);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<ActivityListDTO>>(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<ActivityListDTO>>> GetActivitiesByOpportunityId(int opportunityId)
        {
            try
            {
                var activities = await unitOfWork.ActivityRepository.FindManyAsync(x => x.RelatedOpportunityId == opportunityId);
                var activityDTOs = mapper.Map<IEnumerable<ActivityListDTO>>(activities);
                return new SuccessDataResult<IEnumerable<ActivityListDTO>>(activityDTOs, Messages.SuccessfulOperation);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<ActivityListDTO>>(Messages.ErrorOccurred + " - " + ex.Message);
            }
        }


    }
}
