namespace Api.ViewModels
{
    public class AddressViewModel : EntityViewModel
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }        
        public string State { get; set; }
        public int CityId { get; set; }

    }
}