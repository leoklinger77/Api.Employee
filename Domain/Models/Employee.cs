using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Employee : Entity
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime BirthDate { get; set; }
        public string PathImage { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public JobRole JobRole { get; set; }
        public int JobRoleId { get; set; }
        public IEnumerable<Phone> Phones { get; set; } = new List<Phone>();
        public IEnumerable<Email> Emails { get; set; } = new List<Email>();
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public Address Address { get; set; }

        public Employee() : base() { }        

        public Employee(int id, string fullName, string cpf, string rg, DateTime birthDate, string pathImage, 
                        DateTime insertDate, DateTime? updateDate, JobRole jobRole, Status status, Address address) 
                        : base(id)
        {
            FullName = fullName;
            Cpf = cpf;
            Rg = rg;
            BirthDate = birthDate;
            PathImage = pathImage;
            InsertDate = insertDate;
            UpdateDate = updateDate;
            JobRole = jobRole;
            Status = status;
            Address = address;
        }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   Id == employee.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public int Age()
        {
            int year = DateTime.Now.Year - BirthDate.Year;

            if (DateTime.Now.DayOfYear < BirthDate.DayOfYear)
            {
                year -= 1;
            }
            return year;
        }
        public int HomeTime()
        {
            int year = DateTime.Now.Year - InsertDate.Year;

            if (DateTime.Now.DayOfYear < InsertDate.DayOfYear)
            {
                year -= 1;
            }
            return year;
        }
    }
}
