using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Core.Concretes.DTOs
{
    public class LoginDTO
    {
        [EmailAddress, Required, Display(Name = "Email Address", Prompt = "Email Address")]
        public required string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password", Prompt = "Password")]
        public required string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }


    public class RegisterDTO
    {
        [Required, Display(Name = "First Name", Prompt = "First Name")]
        public required string FirstName { get; set; }

        [Required, Display(Name = "Last Name", Prompt = "Last Name")]
        public required string LastName { get; set; }

        [EmailAddress, Required, Display(Name = "Email Address", Prompt = "Email Address")]
        public required string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password", Prompt = "Password")]
        public required string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password", Prompt = "Confirm Password"), Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }

    public class UserListDTO
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int TotalCustomerCount { get; set; }
        public int TotalLeadCount { get; set; }
        public int TotalActivityCount { get; set; }
        public int TotalOpportunityCount { get; set; }
    }

    public class UserDetailDTO
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public IEnumerable<CustomerListDTO> Customers { get; set; } = [];
        public IEnumerable<LeadListDTO> Leads { get; set; } = [];
    }
}