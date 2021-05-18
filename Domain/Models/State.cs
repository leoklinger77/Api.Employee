using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class State : Entity
    {
        public string Name { get; set; }
        public string Uf { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public IEnumerable<City> Cities { get; set; } = new List<City>();
        public State() : base() { }        

        public State(int id, string name, string uf, DateTime insertDate, DateTime? updateDate) 
                    : base(id)
        {
            Name = name;
            Uf = uf;
            InsertDate = insertDate;
            UpdateDate = updateDate;
        }

        public override bool Equals(object obj)
        {
            return obj is State state &&
                   Id == state.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
