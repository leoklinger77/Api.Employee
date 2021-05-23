using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Authorize]
    [Route("api/v1/Status")]
    public class StatusController : MainController
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IStatusService _statusService;

        public StatusController(IStatusRepository statusRepository, IStatusService statusService,
                                INotifier notifier, IMapper mapper)
                                : base(notifier, mapper)
        {
            _statusRepository = statusRepository;
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAlls()
        {
            return CustomResponse(_mapper.Map<List<StatusViewModel>>(await _statusRepository.FindAlls()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> FindById(int id)
        {
            var rs = _mapper.Map<StatusViewModel>(await _statusRepository.FindById(id));

            if (rs == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }

            return CustomResponse(rs);
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult> Insert(int id, StatusViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                ErrorNotifier("Id informado não confere com o EmployeeId");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _statusService.Insert(_mapper.Map<Status>(viewModel));
            return CustomResponse(viewModel);
        }

        [HttpPut]
        public async Task<ActionResult> Update(StatusViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _statusService.Update(_mapper.Map<Status>(viewModel));
            return CustomResponse(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {            
            if (id == 0 || await _statusRepository.FindById(id) == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }

            await _statusService.Remove(id);
            return CustomResponse();
        }

    }
}
