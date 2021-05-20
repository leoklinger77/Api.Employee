using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {        
        public EmployeeRepository(DataContext context) : base(context) { }

        public async Task<List<Employee>> FindAllsStatusId(int statusIds)
        {
            return await _context.Employee.Where(x => x.StatusId == statusIds).ToListAsync();
        }
    }
}
