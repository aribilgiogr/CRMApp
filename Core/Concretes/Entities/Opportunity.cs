using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    public class Opportunity : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public decimal Value { get; set; }
        public string Currency { get; set; } = null!;
        public int Probability { get; set; }

        public DateTime? ExpectedCloseDate { get; set; }
        public DateTime? ActualCloseDate { get; set; }

        [ForeignKey("AssignedSalesPerson")]
        public string? AssignedSalesPersonId { get; set; }
        public virtual ApplicationUser? AssignedSalesPerson { get; set; }

        public OpportunityStatus Status { get; set; }
        public OpportunityStage Stage { get; set; }


        // Navigation Properties
        public virtual ICollection<Activity> Activities { get; set; } = [];
    }
}
