using System;

namespace Domain.Models
{
    public class Address : Entity
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public string Neighborhood { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public Address() : base() { }        

        public Address(int id,string zipcode, string street, string number, string complement, string reference, 
                       string neighborhood, City city, Employee employee) 
                       : base(id)
        {
            ZipCode = zipcode;
            Street = street;
            Number = number;
            Complement = complement;
            Reference = reference;
            Neighborhood = neighborhood;
            City = city;
            Employee = employee;
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   Id == address.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}