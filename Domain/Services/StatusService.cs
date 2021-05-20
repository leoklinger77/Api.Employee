using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Validation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class StatusService : BaseService, IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public StatusService(INotifier notifier, IEmployeeRepository employeeRepository, IStatusRepository statusRepository) 
                            : base(notifier)
        {
            _employeeRepository = employeeRepository;
            _statusRepository = statusRepository;
        }

        public void Dispose()
        {
            _statusRepository?.Dispose();
        }

        public async Task<bool> Insert(Status status)
        {
            if (!RunValidation(new StatusValidation(), status)) return false;

            if (await _statusRepository.Find(x=>x.Name == status.Name) != null)
            {
                Notify("Name já cadastrado");
                return false;
            }

            await _statusRepository.Insert(status);
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            if (_employeeRepository.FindAllsStatusId(id).Result.Count() >= 1)
            {
                Notify("Status não pode ser deletado, pois possui funcionarios atrelados.");
                return false;
            }                        
            if(await _statusRepository.FindById(id) == null)
            {
                Notify("Id Not found");
                return false;
            }           

            await _statusRepository.Delete(id);
            return true;
        }

        public async Task<bool> Update(Status status)
        {
            if (!RunValidation(new StatusValidation(), status)) return false;

            await _statusRepository.Update(status);
            return true;
        }
    }
}
