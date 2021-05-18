using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IStatusService : IDisposable
    {
        Task<bool> Insert(Status status);
        Task<bool> Update(Status status);
        Task Remove(int id);
    }
}
