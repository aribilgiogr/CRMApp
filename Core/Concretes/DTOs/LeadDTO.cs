using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
    public class LeadFilterDTO
    {
        // Result Data
        public IEnumerable<LeadListDTO> FilteredData { get; set; } = [];

        // Filter:
        public string? Status { get; set; }
        public string? Source { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SearchTerm { get; set; }

        // Pagination:
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Total => FilteredData.Count();
        public int TotalPage => (int)Math.Ceiling((double)Total / PageSize);
    }

    public class LeadListDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Status { get; set; } = null!;
        public string Source { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? AssignedSalesPersonId { get; set; }
        public string? AssignedSalesPersonFullName { get; set; }
    }

    public class LeadCreateDTO
    {
        [Display(Prompt = "FullName"), Required]
        public string FullName { get; set; } = null!;

        [Display(Name = "Company Name", Prompt = "Company Name")]
        public string? CompanyName { get; set; }

        [EmailAddress, Required, Display(Name = "Email Address", Prompt = "Email Address")]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber), Required, Display(Name = "Phone Number", Prompt = "Phone Number")]
        public string Phone { get; set; } = null!;

        [Display(Name = "Lead Source", Prompt = "Lead Source"), Required]
        public LeadSource Source { get; set; }

        [Display(Name = "Additional Notes", Prompt = "Additional Notes"), DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
    }


}
