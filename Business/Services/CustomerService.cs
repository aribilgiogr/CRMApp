using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Core.Concretes.Enums;
using Utilities.Constants;
using Utilities.Results;

namespace Business.Services
{
    public class CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : ICustomerService
    {

        public async Task<IResult> AddAsync(CreateCustomerDTO dto)
        {
            try
            {
                var existingCustomer = await unitOfWork.CustomerRepository.AnyAsync(c => c.Email == dto.Email);
                if (existingCustomer)
                    return new ErrorResult(Messages.EmailAlreadyExists);

                var customer = mapper.Map<Customer>(dto);

                await unitOfWork.CustomerRepository.CreateAsync(customer);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Customer" + Messages.AddedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.FindByIdAsync(id);
                if (customer == null)
                    return new ErrorResult("Customer" + Messages.NotFoundSuffix);

                await unitOfWork.CustomerRepository.DeleteAsync(customer);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Customer" + Messages.DeletedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetActiveCustomersAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.Status == CustomerStatus.Active);
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, "Active customers" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetAllAsync()
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync();
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, "Customers" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByAssignedSalesPersonAsync(string salesPersonId)
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.AssignedSalesPersonId == salesPersonId);
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, "Customers assigned to sales person" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByCityAsync(string city)
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.City == city);
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, $"Customers in {city} " + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByCompanyNameAsync(string companyName)
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.CompanyName == companyName);
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, $"Customers with company name {companyName} " + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<CustomerDetailDTO>> GetByIdAsync(int id)
        {
            try
            {
                var customer = await unitOfWork.CustomerRepository.FindByIdAsync(id);
                if (customer == null)
                    return new ErrorDataResult<CustomerDetailDTO>("Customer" + Messages.NotFoundSuffix);
                var customerDTO = mapper.Map<CustomerDetailDTO>(customer);
                return new SuccessDataResult<CustomerDetailDTO>(customerDTO, "Customer" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CustomerDetailDTO>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByStatusAsync(CustomerStatus status)
        {
            try
            {
                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c => c.Status == status);
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, $"Customers with status {status} " + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<CustomerListDTO>>> SearchCustomersAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                    return new ErrorDataResult<IEnumerable<CustomerListDTO>>("Search term cannot be empty.");

                var customers = await unitOfWork.CustomerRepository.FindManyAsync(c =>
                    c.CompanyName.Contains(searchTerm) ||
                    c.Email.Contains(searchTerm) ||
                    (c.City != null && c.City.Contains(searchTerm)) ||
                    (c.Country != null && c.Country.Contains(searchTerm))
                );
                var customerDTOs = mapper.Map<IEnumerable<CustomerListDTO>>(customers);
                return new SuccessDataResult<IEnumerable<CustomerListDTO>>(customerDTOs, $"Customers matching '{searchTerm}' " + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<CustomerListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IResult> UpdateAsync(UpdateCustomerDTO dto)
        {
            try
            {
                var existingCustomer = await unitOfWork.CustomerRepository.FindByIdAsync(dto.Id);
                if (existingCustomer == null)
                    return new ErrorResult("Customer" + Messages.NotFoundSuffix);

                var emailExists = await unitOfWork.CustomerRepository.AnyAsync(c => c.Email == dto.Email && c.Id != dto.Id);
                if (emailExists)
                    return new ErrorResult(Messages.EmailAlreadyExists);

                mapper.Map(dto, existingCustomer);
                await unitOfWork.CustomerRepository.UpdateAsync(existingCustomer);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Customer" + Messages.UpdatedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }
    }
}
