﻿using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmployeeServices : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IPhoneRepository _phoneRepository;
        public EmployeeServices(INotifier notifier, IEmployeeRepository employeeRepository, IAddressRepository addressRepository,
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
            RunValidation(new EmployeeValidation(), employee);
            RunValidation(new AddressValidation(), employee.Address);
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

            if (_notifier.HasNotification())
                return false;

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
            RunValidation(new EmployeeValidation(), employee);

            if (employee.Address != null) RunValidation(new AddressValidation(), employee.Address);

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

            if (_notifier.HasNotification())
                return false;

            await _employeeRepository.Update(employee);
            return true;

        }

        public async Task<bool> UpdateAddress(Address address)
        {
            if (!RunValidation(new AddressValidation(), address)) return false;

            await _addressRepository.Update(address);
            return true;
        }
    }
}
