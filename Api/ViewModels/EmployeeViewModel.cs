using Api.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.ViewModels
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "Employee")]
    public class EmployeeViewModel : EntityViewModel
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string PathImage { get; set; }
        public string Password { get; set; }
        public JobRoleViewModel JobRole { get; set; }
        public int JobRoleId { get; set; }
        public IEnumerable<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
        public IEnumerable<EmailViewModel> Emails { get; set; } = new List<EmailViewModel>();
        public StatusViewModel Status { get; set; }
        public int StatusId { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
