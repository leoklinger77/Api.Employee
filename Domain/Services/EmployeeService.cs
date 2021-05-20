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
        public EmployeeService(INotifier notifier, IEmployeeRepository employeeRepository, IAddressRepository addressRepository,
                                IEmailRepository emailRepository, IPhoneRepository phoneRepository)
                                : base(notifier)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _emailRepository = emailRepository;
            _phoneRepository = phoneRepository;
        }

        public void Dispose()
        {
            _employeeRepository?.Dispose();
        }
        public async Task<bool> Insert(Employee employee)
        {
            if(!RunValidation(new EmployeeValidation(), employee)) return false;
            if(!RunValidation(new AddressValidation(), employee.Address)) return false;
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
            if(!RunValidation(new EmployeeValidation(), employee)) return false;

            if (employee.Address != null) 
                if(!RunValidation(new AddressValidation(), employee.Address)) return false;

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
