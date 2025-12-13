using Core.Concretes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class CrmDbContext : DbContext
    {
        public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Opportunity> Opportunities { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
    }
}
