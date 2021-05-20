namespace Api.ViewModels
{
    public class EmailViewModel : EntityViewModel
    {
        public string EmailAddress { get; set; }
        public int EmailType { get; set; }        
        public int Employeeid { get; set; }
    }
}