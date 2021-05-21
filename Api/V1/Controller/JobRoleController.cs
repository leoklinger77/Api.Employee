using Api.Controllers;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Route("api/v1/JobRole")]
    public class JobRoleController : MainController
    {
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IJobRoleService _jobRoleService;
        public JobRoleController(INotifier notifier, IMapper mapper, IJobRoleRepository jobRoleRepository, IJobRoleService jobRoleService) : base(notifier, mapper)
        {
            _jobRoleRepository = jobRoleRepository;
            _jobRoleService = jobRoleService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAlls()
        {
            return CustomResponse(_mapper.Map<List<JobRoleViewModel>>(await _jobRoleRepository.FindAlls()));
        }

        [HttpPost]
        public async Task<ActionResult> Inset(JobRoleViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);            

            await _jobRoleService.Insert(_mapper.Map<JobRole>(viewModel));

            return CustomResponse(_mapper.Map<JobRoleViewModel>(viewModel));
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> FindById(int id)
        {
            var rs = await _jobRoleRepository.FindById(id);
            if (id == 0 || rs == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }
            return CustomResponse(_mapper.Map<JobRoleViewModel>(rs));
        }
        [HttpPut]
        public async Task<ActionResult> Update(JobRoleViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _jobRoleService.Update(_mapper.Map<JobRole>(viewModel));

            return CustomResponse(viewModel);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0 || await _jobRoleRepository.FindById(id) == null)
            {
                ErrorNotifier("Id Not Found");
                return CustomResponse();
            }

            await _jobRoleRepository.Delete(id);
             
            return CustomResponse();
        }
    }
}
