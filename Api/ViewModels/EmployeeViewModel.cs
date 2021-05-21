using System;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class EmployeeViewModel : EntityViewModel
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImageUpload { get; set; }
        public string PathImage { get; set; }        
        public JobRoleViewModel JobRole { get; set; }
        public int JobRoleId { get; set; }
        public IEnumerable<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
        public IEnumerable<EmailViewModel> Emails { get; set; } = new List<EmailViewModel>();
        public StatusViewModel Status { get; set; }
        public int StatusId { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
