using Microsoft.AspNetCore.Identity;

namespace Core.Concretes.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Default properties from IdentityUser: Id (string), UserName (string), NormalizedUserName (string), Email (string), NormalizedEmail (string), EmailConfirmed (bool), PasswordHash (string), SecurityStamp (string), ConcurrencyStamp (string), PhoneNumber (string)

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public bool IsAdmin { get; set; } = false;

        public virtual ICollection<Customer> Customers { get; set; } = [];
        public virtual ICollection<Lead> Leads { get; set; } = [];
        public virtual ICollection<Activity> Activities { get; set; } = [];
        public virtual ICollection<Opportunity> Opportunities { get; set; } = [];

    }
}
