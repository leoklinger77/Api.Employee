using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeService : IDisposable
    {
        Task<bool> Insert(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Remove(int id);
        Task<bool> UpdateAddress(Address address);
    }
}
