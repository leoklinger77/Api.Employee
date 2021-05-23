using Api.Controllers;
using Api.Extension;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Authorize]
    [Route("api/v1/Employee")]
    public class EmployeeController : MainController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly FileService _fileService;
        private readonly IdentityTokenManipulation _identityToken;
        private readonly DataContext _dataContext;
        public EmployeeController(INotifier notifier, IMapper mapper, IEmployeeRepository employeeRepository, IEmployeeService employeeService, FileService fileService, IdentityTokenManipulation identityToken, DataContext dataContext) : base(notifier, mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _fileService = fileService;
            _identityToken = identityToken;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeViewModel>>> FindAll()
        {
            return CustomResponse(_mapper.Map<List<EmployeeViewModel>>(await _employeeRepository.FindAllsInclude()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<EmployeeViewModel>>> FindById(int id)
        {
            var rs = await _employeeRepository.FindByIdInclude(id);
            if (id == 0 || rs == null)
            {
                ErrorNotifier("Id not found");
                return CustomResponse();
            }
            return CustomResponse(_mapper.Map<EmployeeViewModel>(rs));
        }

        [HttpPost]
        [RequestSizeLimit(5000000)]
        public async Task<ActionResult> Insert(EmployeeViewModel viewModel)
        {
            var beginTrans = _dataContext.Database.BeginTransactionAsync();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //Grava Imagem
            if (viewModel.ImageUpload != null && viewModel.ImageUpload.Length > 0)
            {
                string imageName = Guid.NewGuid() + "";
                new Thread(() =>
                {
                    _fileService.UpdateFile(viewModel.ImageUpload, imageName);
                }).Start();
                viewModel.PathImage = imageName + viewModel.ImageUpload.FileName;
            }
            //Cadastra no Banco
            await _employeeService.Insert(_mapper.Map<Employee>(viewModel));

            //valida se a operacao foi valida
            if (!OperationValid())
            {
                new Thread(() =>
                {
                    _fileService.DeleteFile(viewModel.PathImage);
                }).Start();
                return CustomResponse();
            }
            else
            {
                //cadastra Identity
                if (!await _identityToken.RegisterAccount(new RegisterUserViewModel
                {
                    Email = viewModel.Emails.First().EmailAddress,
                    Name = viewModel.Cpf,
                    Password = viewModel.Password
                }))
                {
                    await beginTrans.Result.RollbackAsync();
                    return CustomResponse();
                }
                await beginTrans.Result.CommitAsync();
                return CustomResponse();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, EmployeeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                ErrorNotifier("Id informado não confere com o EmployeeId");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _employeeService.Update(_mapper.Map<Employee>(viewModel));

            return CustomResponse(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0 || await _employeeRepository.FindById(id) == null)
            {
                ErrorNotifier("Id not found");
                return CustomResponse();
            }

            await _employeeService.Remove(id);

            return CustomResponse();
        }

    }
}
