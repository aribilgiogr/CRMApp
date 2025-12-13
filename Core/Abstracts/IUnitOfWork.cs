using Core.Abstracts.IRepositories;

namespace Core.Abstracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IContactRepository ContactRepository { get; }
        ILeadRepository LeadRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IOpportunityRepository OpportunityRepository { get; }

        Task CommitAsync();
    }
}
