using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IJobRoleService
    {
        Task<bool> Insert(JobRole jobRole);
        Task<bool> Update(JobRole jobRole);
        Task<bool> Remove(int id);
    }
}
