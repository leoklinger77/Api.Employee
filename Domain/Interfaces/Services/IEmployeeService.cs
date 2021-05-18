using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<bool> Insert(Employee employee);
        Task<bool> InsertEmail(int employeeId, Email email);
        Task<bool> InsertPhone(int employeeId, Phone phone);
        
        Task<bool> Update(Employee employee);
        Task<bool> UpdateEmail(int employeeId, Email email);
        Task<bool> UpdatePhone(int employeeId, Phone phone);
        Task<bool> UpdateAddress(int employeeId, Address address);

        Task<bool> Remove(int id);
        
    }
}
