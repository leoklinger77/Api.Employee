using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Status : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool AllowLogin { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
        public Status() : base() { }        

        public Status(int id, string name, string description, bool active, bool allowLogin, DateTime insertDate, DateTime? updateDate) 
                      : base(id)
        {
            Name = name;
            Description = description;
            Active = active;
            AllowLogin = allowLogin;
            InsertDate = insertDate;
            UpdateDate = updateDate;
        }

        public override bool Equals(object obj)
        {
            return obj is Status status &&
                   Id == status.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
