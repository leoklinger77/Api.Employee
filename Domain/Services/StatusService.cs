using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StatusService : BaseService, IStatusService
    {
        
        public StatusService(INotifier notifier) : base(notifier)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Status status)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Status status)
        {
            throw new NotImplementedException();
        }
    }
}
