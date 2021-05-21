using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class JobRoleService : BaseService, IJobRoleService
    {
        private readonly IJobRoleRepository _jobRoleRepository;

        public JobRoleService(INotifier notifier, IJobRoleRepository jobRoleRepository) : base(notifier)
        {
            _jobRoleRepository = jobRoleRepository;
        }

        public async Task<bool> Insert(JobRole jobRole)
        {
            if (!RunValidation(new JobRoleValidation(), jobRole)) return false;

            if (_jobRoleRepository.Find(x => x.Name == jobRole.Name.Trim()).Result.Count() >= 1)
            {
                Notify("Cargo já cadastrado");
                return false;
            }

            await _jobRoleRepository.Insert(jobRole);
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            if (id == 0 || await _jobRoleRepository.FindById(id) == null)
            {
                Notify("Id not Found");
                return false;
            }

            await _jobRoleRepository.Delete(id);
            return true;
        }

        public async Task<bool> Update(JobRole jobRole)
        {
            if (!RunValidation(new JobRoleValidation(), jobRole)) return false;

            await _jobRoleRepository.Update(jobRole);
            return true;
        }
    }
}
