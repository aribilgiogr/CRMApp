using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Enums;
using Utilities.Constants;
using Utilities.Results;

namespace Business.Services
{
    public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
    {
        public Task<IResult> AddAsync(CustomerDTO customerDto)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<IEnumerable<CustomerDTO>>> GetActiveCustomersAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.Status == CustomerStatus.Active);
                var customerDtos = mapper.Map<IEnumerable<CustomerDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerDTO>>(customerDtos, $"Active Customers {Messages.RetrievedSuffix}");
            }
            catch (Exception)
            {
                return new ErrorDataResult<IEnumerable<CustomerDTO>>($"{Messages.ErrorOccurred}");
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync();
                var customerDtos = mapper.Map<IEnumerable<CustomerDTO>>(customers);

                return new SuccessDataResult<IEnumerable<CustomerDTO>>(customerDtos, $"Customers {Messages.RetrievedSuffix}");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerDTO>>($"{Messages.ErrorOccurred}: {ex.Message}");
            }
        }

        public async Task<IDataResult<CustomerDTO>> GetByIdAsync(int id)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.FindByIdAsync(id);
                if (customer == null)
                {
                    return new ErrorDataResult<CustomerDTO>($"Customer {Messages.NotFoundSuffix}");
                }
                var customerDto = mapper.Map<CustomerDTO>(customer);
                return new SuccessDataResult<CustomerDTO>(customerDto, $"Customer {Messages.RetrievedSuffix}");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CustomerDTO>($"{Messages.ErrorOccurred}: {ex.Message}");
            }
        }

        public Task<IDataResult<IEnumerable<CustomerDTO>>> SearchCustomersAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateAsync(CustomerDTO customerDto)
        {
            throw new NotImplementedException();
        }
    }
}
