using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class JobRole : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

        public JobRole() : base() { }        

        public JobRole(int id, string name, string description, DateTime insertDate, DateTime? updateDate) 
                       : base(id)
        {
            Name = name;
            Description = description;
            InsertDate = insertDate;
            UpdateDate = updateDate;
        }

        public override bool Equals(object obj)
        {
            return obj is JobRole role &&
                   Id == role.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
