using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class JobRoleRepository : Repository<JobRole>, IJobRoleRepository
    {
        public JobRoleRepository(DataContext context) : base(context) { }
        
    }
}
