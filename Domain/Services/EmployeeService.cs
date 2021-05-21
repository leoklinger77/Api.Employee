using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Domain.Models.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IJobRoleRepository _jobRoleRepository;

        public EmployeeService(INotifier notifier, IEmployeeRepository employeeRepository, IAddressRepository addressRepository,
                                IEmailRepository emailRepository, IPhoneRepository phoneRepository, IStatusRepository statusRepository, 
                                ICityRepository cityRepository, IJobRoleRepository jobRoleRepository)
                                : base(notifier)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
            _statusRepository = statusRepository;
            _cityRepository = cityRepository;
            _jobRoleRepository = jobRoleRepository;
        }

        public void Dispose()
        {
            _employeeRepository?.Dispose();
        }
        public async Task<bool> Insert(Employee employee)
        {
            RunValidation(new EmployeeValidation(), employee);
            RunValidation(new AddressValidation(), employee.Address);

            if (employee.Emails.Count() >= 1)
            {
                foreach (var email in employee.Emails)
                    RunValidation(new EmailValidation(), email);
            }
            else
            {
                Notify("Necessario no minimo 1 e-mail");
            }

            if (employee.Phones.Count() >= 1)
            {
                foreach (var phone in employee.Phones)
                    RunValidation(new PhoneValidation(), phone);
            }
            else
            {
                Notify("Necessario no minimo 1 telefone");
            }

            if (employee.JobRoleId == 0 || _jobRoleRepository.Find(x => x.Id == employee.JobRoleId).Result.Count() == 0)
            {
                Notify("JobRoleId inválido");
            }

            if (employee.StatusId == 0 || _statusRepository.Find(x => x.Id == employee.StatusId).Result.Count() == 0)
            {
                Notify("StatusId inválido");
            }

            if (employee.Address.CityId == 0 || _cityRepository.Find(x => x.Id == employee.Address.CityId).Result.Count() == 0)
            {
                Notify("CityId inválida");
            }

            if (_employeeRepository.Find(x => x.Cpf == employee.Cpf).Result.Count() >= 1)
            {
                Notify("Cpf já cadastrado");
            }


            if (_notifier.HasNotification()) return false;

            employee.StatusId = 1004;


            await _employeeRepository.Insert(employee);
            return true;
        }
        public async Task<bool> Remove(int id)
        {
            var employee = await _employeeRepository.FindById(id);

            if (employee == null)
            {
                Notify("Funcionario não localizado");
                return false;
            }

            if (employee.Emails.Count() >= 1)
            {
                new Thread(() =>
                {
                    foreach (var item in employee.Emails)
                    {
                        _emailRepository.Delete(item.Id);
                    }
                }).Start();
            }

            if (employee.Phones.Count() >= 1)
            {
                new Thread(() =>
                {
                    foreach (var item in employee.Phones)
                    {
                        _phoneRepository.Delete(item.Id);
                    }
                }).Start();
            }

            await _employeeRepository.Delete(id);
            return true;

        }
        public async Task<bool> Update(Employee employee)
        {
            if (!RunValidation(new EmployeeValidation(), employee)) return false;

            if (employee.Address != null)
                if (!RunValidation(new AddressValidation(), employee.Address)) return false;

            if (employee.Emails.Count() >= 1)
            {
                foreach (var email in employee.Emails)
                    RunValidation(new EmailValidation(), email);
            }

            if (employee.Phones.Count() >= 1)
            {
                foreach (var phone in employee.Phones)
                    RunValidation(new PhoneValidation(), phone);
            }



            await _employeeRepository.Update(employee);
            return true;

        }
        public async Task<bool> UpdateAddress(int employeeId, Address address)
        {
            if (!RunValidation(new AddressValidation(), address)) return false;

            if (employeeId != address.EmployeeId)
            {
                Notify("Id informado não confere com o EmployeeId");
                return false;
            }

            await _addressRepository.Update(address);
            return true;
        }
        public async Task<bool> InsertEmail(int employeeId, Email email)
        {
            if (!RunValidation(new EmailValidation(), email)) return false;

            if (employeeId != email.EmployeeId)
            {
                Notify("Id informado não confere com o EmployeeId");
                return false;
            }

            await _emailRepository.Insert(email);
            return true;
        }
        public async Task<bool> InsertPhone(int employeeId, Phone phone)
        {
            if (!RunValidation(new PhoneValidation(), phone)) return false;

            if (employeeId != phone.EmployeeId)
            {
                Notify("Id informado não confere com o EmployeeId");
                return false;
            }

            await _phoneRepository.Insert(phone);
            return true;
        }
        public async Task<bool> UpdateEmail(int employeeId, Email email)
        {
            if (!RunValidation(new EmailValidation(), email)) return false;

            if (employeeId != email.EmployeeId)
            {
                Notify("Id informado não confere com o EmployeeId");
                return false;
            }

            await _emailRepository.Update(email);
            return true;
        }
        public async Task<bool> UpdatePhone(int employeeId, Phone phone)
        {
            if (!RunValidation(new PhoneValidation(), phone)) return false;

            if (employeeId != phone.EmployeeId)
            {
                Notify("Id informado não confere com o EmployeeId");
                return false;
            }

            await _phoneRepository.Update(phone);
            return true;
        }
    }
}
