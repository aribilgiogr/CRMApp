using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class CreateCustomerDTO
    {
        [Required]
        public int LeadId { get; set; }
        [Required]
        public string CompanyName { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Industry { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AssignedSalesPersonId { get; set; }
        [Required]
        public CustomerStatus Status { get; set; }
    }

    public class UpdateCustomerDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Industry { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AssignedSalesPersonId { get; set; }
        [Required]
        public CustomerStatus Status { get; set; }
    }

    public class CustomerListDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string Status { get; set; } = null!;
        public int TotalOpportunities { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class CustomerDetailDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? TaxNumber { get; set; }
        public string? Industry { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AssignedSalesPersonId { get; set; }
        public string? AssignedSalesPersonName { get; set; }
        public string Status { get; set; } = null!;
        public int TotalOpportunities { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<ContactListDTO> Contacts { get; set; } = [];
        public IEnumerable<ActivityListDTO> Activities { get; set; } = [];
        public IEnumerable<OpportunityListDTO> Opportunities { get; set; } = [];
    }
}
