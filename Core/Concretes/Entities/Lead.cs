using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    public class Lead : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public LeadStatus Status { get; set; } = LeadStatus.New;
        public LeadSource Source { get; set; }
        public int Score { get; set; }
        public string? Notes { get; set; }

        [ForeignKey("ConvertedToCustomer")]
        public int? ConvertedToCustomerId { get; set; }
        public virtual Customer? ConvertedToCustomer { get; set; }

        public DateTime? ConvertedDate { get; set; }

        [ForeignKey("AssignedSalesPerson")]
        public string? AssignedSalesPersonId { get; set; }
        public virtual ApplicationUser? AssignedSalesPerson { get; set; }

        // Navigation Properties
        public virtual ICollection<Activity> Activities { get; set; } = [];
    }
}
