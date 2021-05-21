using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        public async Task<ActionResult> Insert(EmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + viewModel.PathImage + ".jpg";
            if (!UpdateFile(viewModel.ImageUpload, imageName))
            {
                return CustomResponse();
            }
            viewModel.PathImage = imageName;

            await _employeeService.Insert(_mapper.Map<Employee>(viewModel));

            if (!OperationValid())
            {
                new Thread(() =>
                {
                    DeleteFile(viewModel.PathImage);
                }).Start();
            }

            return CustomResponse(viewModel);
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

        private bool UpdateFile(string file, string imgName)
        {
            if (string.IsNullOrEmpty(file))
            {
                ErrorNotifier("Forneça uma imagem para este produto!");
                return false;
            }
            var imageDataByteArray = Convert.FromBase64String(file);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgName);

            if (System.IO.File.Exists(filePath))
            {
                ErrorNotifier("Já existe um arquivo com esse nome!");
                return false;
            }
            new Thread(() =>
            {
                System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            }).Start();
            return true;
        }
        private void DeleteFile(string pathImage)
        {
            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", pathImage));
            try
            {
                fi.Delete();
            }
            catch (IOException)
            {
                ErrorNotifier("Error ao excluir o arquivo enviado para o ServerFile.");
            }
        }
    }
}
