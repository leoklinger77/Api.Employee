using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context) { }
    }
}
