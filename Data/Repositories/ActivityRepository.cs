using Core.Abstracts.IRepositories;
using Core.Concretes.Entities;
using Data.Contexts;
using Utilities.Generics;

namespace Data.Repositories
{
    public class ActivityRepository(CrmDbContext context) : Repository<Activity>(context), IActivityRepository
    {
    }
}
