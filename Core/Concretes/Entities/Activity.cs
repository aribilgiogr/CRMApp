using Core.Abstracts.Bases;
using Core.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    public class Activity : BaseEntity
    {
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public int? RelatedCustomerId { get; set; }
        public virtual Customer? RelatedCustomer { get; set; }

        public int? RelatedLeadId { get; set; }
        public virtual Lead? RelatedLead { get; set; }

        public int? RelatedOpportunityId { get; set; }
        public virtual Opportunity? RelatedOpportunity { get; set; }

        [ForeignKey("AssignedSalesPerson")]
        public string? AssignedSalesPersonId { get; set; }
        public virtual ApplicationUser? AssignedSalesPerson { get; set; }

        public ActivityType Type { get; set; }
    }
}
