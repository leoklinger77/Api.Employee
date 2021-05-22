using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Validation;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private readonly IEmailRepository _emailRepository;

        public EmailService(INotifier notifier, IEmailRepository emailRepository) : base(notifier)
        {
            _emailRepository = emailRepository;
        }

        public async Task<bool> Insert(Email email)
        {
            if (!RunValidation(new EmailValidation(), email)) return false;

            await _emailRepository.Insert(email);

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            if (id == 0 || await _emailRepository.FindById(id) == null)
            {
                Notify("Id Not Found");
                return false;
            }

            await _emailRepository.Delete(id);
            return true;
        }

        public async Task<bool> Update(Email email)
        {
            if (!RunValidation(new EmailValidation(), email)) return false;

            await _emailRepository.Update(email);

            return true;
        }
    }
}
