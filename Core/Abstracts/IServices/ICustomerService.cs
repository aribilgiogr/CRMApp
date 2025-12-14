using Core.Concretes.DTOs;
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
        Task<IDataResult<IEnumerable<CustomerDTO>>> GetAllAsync();
        Task<IDataResult<CustomerDTO>> GetByIdAsync(int id);
        Task<IResult> AddAsync(CustomerDTO customerDto);
        Task<IResult> UpdateAsync(CustomerDTO customerDto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<IEnumerable<CustomerDTO>>> SearchCustomersAsync(string searchTerm);
        Task<IDataResult<IEnumerable<CustomerDTO>>> GetActiveCustomersAsync();
    }
}
