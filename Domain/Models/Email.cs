using Domain.Models.Enumerations;
using System;

namespace Domain.Models
{
    public class Email : Entity
    {
        public string EmailAddress { get; set; }
        public EmailType EmailType { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Email() : base() { }        

        public Email(int id, string emailAddress, EmailType emailType, DateTime insertDate, DateTime? updateDate, Employee employee) 
                    : base(id)
        {
            EmailAddress = emailAddress;
            EmailType = emailType;
            InsertDate = insertDate;
            UpdateDate = updateDate;
            Employee = employee;
        }

        public override bool Equals(object obj)
        {
            return obj is Email email &&
                   Id == email.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
