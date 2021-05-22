using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Route("api/v1/Email")]
    public class EmailController : MainController
    {
        private readonly IEmailService _emailService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailRepository _emailRepository;

        public EmailController(INotifier notifier, IMapper mapper, IEmailService emailService, IEmployeeRepository employeeRepository, IEmailRepository emailRepository) : base(notifier, mapper)
        {
            _emailService = emailService;
            _employeeRepository = employeeRepository;
            _emailRepository = emailRepository;
        }

        [HttpPost("{EmployeeId:int}")]
        public async Task<ActionResult> Insert(int employeeId, EmailViewModel viewModel)
        {
            var employee = await _employeeRepository.FindByIdInclude(employeeId);
            if (employeeId == 0 || employee == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }
            if (employee.Emails.Count() >= 2)
            {
                ErrorNotifier("número máximo de e-mail atingido");
                return CustomResponse();
            }
            await _emailService.Insert(_mapper.Map<Email>(viewModel));

            return CustomResponse();

        }
        [HttpPut("{EmployeeId:int}/{id:int}")]
        public async Task<ActionResult> Update(int employeeId,int id, EmailViewModel viewModel)
        {
            var email = await _emailRepository.FindById(id);
            if (employeeId == 0 || id == 0 || employeeId != viewModel.Employeeid || id != viewModel.Id || email == null)
            {
                return CustomResponse("Id Not Found");
            }
            await _emailService.Update(_mapper.Map<Email>(viewModel));

            return CustomResponse();
        }

        [HttpDelete("{EmployeeId:int}/{id:int}")]
        public async Task<ActionResult> Delete(int employeeId, int id)
        {
            var employee = await _employeeRepository.FindByIdInclude(employeeId);
            if (employeeId == 0 || employee == null || employee.Emails.FirstOrDefault(x=>x.Id == id) == null)
            {
                return CustomResponse("Id Not Found");
            }
            if(employee.Emails.Count() == 1)            
                return CustomResponse("E-mail não pode ser excluido, violação de regra de negocio.");
            
            await _emailService.Remove(id);
            return CustomResponse();
        }
    }
}
