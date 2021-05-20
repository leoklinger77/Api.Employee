using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> FindAllsStatusId(int statusIds);
    }
}
