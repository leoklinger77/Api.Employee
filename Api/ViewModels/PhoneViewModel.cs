namespace Api.ViewModels
{
    public class PhoneViewModel : EntityViewModel
    {
        public string Ddd { get; set; }
        public string Number { get; set; }
        public int PhoneType { get; set; }        
        public int Employeeid { get; set; }
    }
}