using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Concretes.DTOs
{
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
