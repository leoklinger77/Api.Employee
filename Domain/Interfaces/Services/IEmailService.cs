using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> Insert(Email email);
        Task<bool> Update(Email email);
        Task<bool> Remove(int id);
    }
}
