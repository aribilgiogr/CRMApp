using Core.Abstracts.Bases;
using Core.Concretes.Enums;

namespace Core.Concretes.Entities
{
    public class Customer : BaseEntity
    {
        public required string CompanyName { get; set; }
        public string? TaxNumber { get; set; }
        public string? Industry { get; set; }

        // required property: Nesne oluşturulurken mutlaka bir değer atanmalıdır.
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? AssignedSalesPersonId { get; set; }
        public required CustomerStatus Status { get; set; }


        // Navigation Properties
        public virtual ICollection<Contact> Contacts { get; set; } = [];
        public virtual ICollection<Opportunity> Opportunities { get; set; } = [];
        public virtual ICollection<Activity> Activities { get; set; } = [];
        public virtual ICollection<Lead> Leads { get; set; } = [];
    }
}
