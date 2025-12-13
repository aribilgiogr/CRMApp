using Core.Abstracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? GSM { get; set; }
        public bool IsPrimary { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
