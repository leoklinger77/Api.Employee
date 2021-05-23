using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Authorize]
    [Route("api/v1/Phone")]
    public class PhoneController : MainController
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPhoneService _phoneService;
        private readonly IPhoneRepository _phoneRepository;
        public PhoneController(INotifier notifier, IMapper mapper, IEmployeeRepository employeeRepository, 
                                IPhoneService phoneService, IPhoneRepository phoneRepository) 
                                : base(notifier, mapper)
        {
            _employeeRepository = employeeRepository;
            _phoneService = phoneService;
            _phoneRepository = phoneRepository;
        }

        [HttpPost("{EmployeeId:int}")]
        public async Task<ActionResult> Insert(int employeeId, PhoneViewModel viewModel)
       {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var employee = await _employeeRepository.FindByIdInclude(employeeId);
            if (employeeId == 0 || employee == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }
            if (employee.Phones.Count() >= 3)
            {
                ErrorNotifier("número máximo de telefones atingido");
                return CustomResponse();
            }
            await _phoneService.Insert(_mapper.Map<Phone>(viewModel));

            return CustomResponse();

        }
        [HttpPut("{EmployeeId:int}/{id:int}")]
        public async Task<ActionResult> Update(int employeeId, int id, PhoneViewModel viewModel)
        {
            var email = await _phoneRepository.FindById(id);
            if (employeeId == 0 || id == 0 || employeeId != viewModel.Employeeid || id != viewModel.Id || email == null)
            {
                return CustomResponse("Id Not Found");
            }
            await _phoneService.Update(_mapper.Map<Phone>(viewModel));

            return CustomResponse();
        }

        [HttpDelete("{EmployeeId:int}/{id:int}")]
        public async Task<ActionResult> Delete(int employeeId, int id)
        {
            var employee = await _employeeRepository.FindByIdInclude(employeeId);
            if (employeeId == 0 || employee == null || employee.Phones.FirstOrDefault(x => x.Id == id) == null)
            {
                return CustomResponse("Id Not Found");
            }
            if (employee.Phones.Count() == 1)
                return CustomResponse("Telefone não pode ser excluido, violação de regra de negocio.");

            await _phoneService.Remove(id);
            return CustomResponse();
        }
    }
}
