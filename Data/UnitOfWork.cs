using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Data.Contexts;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork(CrmDbContext context) : IUnitOfWork
    {
        private ICustomerRepository? customerRepository;
        public ICustomerRepository CustomerRepository => customerRepository ??= new CustomerRepository(context);

        private IContactRepository? contactRepository;
        public IContactRepository ContactRepository => contactRepository ??= new ContactRepository(context);

        private ILeadRepository? leadRepository;
        public ILeadRepository LeadRepository => leadRepository ??= new LeadRepository(context);

        private IActivityRepository? activityRepository;
        public IActivityRepository ActivityRepository => activityRepository ??= new ActivityRepository(context);

        private IOpportunityRepository? opportunityRepository;
        public IOpportunityRepository OpportunityRepository => opportunityRepository ??= new OpportunityRepository(context);

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //await DisposeAsync();
                throw new Exception("An error occurred while committing the transaction.", ex);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
