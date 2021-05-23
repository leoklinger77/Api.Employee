namespace Api.ViewModels
{
    public class RegisterUserViewModel
    {        
        public string Email { get; set; }   
        public string Password { get; set; }        
        public string Name { get; set; }
    }
    public class LoginUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }        
    }
}
