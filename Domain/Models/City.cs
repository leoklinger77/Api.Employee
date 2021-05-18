using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class City : Entity
    {
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public State State { get; set; }
        public IEnumerable<Address> Address { get; set; } = new List<Address>();

        public City() : base() { }        

        public City(int id, string name, DateTime insertDate, DateTime? updateDate, State state) 
                    : base(id)
        {
            Name = name;
            InsertDate = insertDate;
            UpdateDate = updateDate;
            State = state;
        }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   Id == city.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}