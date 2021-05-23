using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Validation;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PhoneService : BaseService, IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;

        public PhoneService(INotifier notifier, IPhoneRepository phoneRepository) : base(notifier)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<bool> Insert(Phone phone)
        {
            if (!RunValidation(new PhoneValidation(), phone)) return false;

            await _phoneRepository.Insert(phone);

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            if (id == 0 || await _phoneRepository.FindById(id) == null)
            {
                Notify("Id Not Found");
                return false;
            }

            await _phoneRepository.Delete(id);
            return true;
        }

        public async Task<bool> Update(Phone phone)
        {
            if (!RunValidation(new PhoneValidation(), phone)) return false;

            await _phoneRepository.Update(phone);

            return true;
        }
    }
}
