using Domain.Models.Enumerations;
using System;

namespace Domain.Models
{
    public class Phone : Entity
    {
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Phone() : base() { }        

        public Phone(int id, string ddd, string number, PhoneType phoneType, DateTime insertDate, DateTime? updateDate, Employee employee) 
                     : base(id)
        {
            Ddd = ddd;
            Number = number;
            PhoneType = phoneType;
            InsertDate = insertDate;
            UpdateDate = updateDate;
            Employee = employee;
        }

        public override bool Equals(object obj)
        {
            return obj is Phone phone &&
                   Id == phone.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
