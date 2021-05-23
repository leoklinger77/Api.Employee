using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IPhoneService
    {
        Task<bool> Insert(Phone phone);
        Task<bool> Update(Phone phone);
        Task<bool> Remove(int id);
    }
}
