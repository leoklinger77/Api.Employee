using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Route("api/v1/Employee")]
    public class EmployeeController : MainController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(INotifier notifier, IMapper mapper, IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(notifier, mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var map = await _employeeRepository.FindAlls();
            return CustomResponse(_mapper.Map<EmployeeViewModel>(map));
        }
        [HttpPost]
        public async Task<ActionResult> Insert(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _employeeService.Insert(_mapper.Map<Employee>(employee));

            return CustomResponse(employee);
        }

    }
}
