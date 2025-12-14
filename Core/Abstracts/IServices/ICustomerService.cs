using Core.Concretes.DTOs;
using Core.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Core.Abstracts.IServices
{
    public interface ICustomerService
    {
        // CRUD Operations
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetAllAsync();
        Task<IDataResult<CustomerDetailDTO>> GetByIdAsync(int id);
        Task<IResult> AddAsync(CreateCustomerDTO dto);
        Task<IResult> UpdateAsync(UpdateCustomerDTO dto);
        Task<IResult> DeleteAsync(int id);

        // Filtering and Searching
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByStatusAsync(CustomerStatus status);
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetActiveCustomersAsync();
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByCompanyNameAsync(string companyName);
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByCityAsync(string city);
        Task<IDataResult<IEnumerable<CustomerListDTO>>> SearchCustomersAsync(string searchTerm);
        Task<IDataResult<IEnumerable<CustomerListDTO>>> GetByAssignedSalesPersonAsync(string salesPersonId);
    }
}
