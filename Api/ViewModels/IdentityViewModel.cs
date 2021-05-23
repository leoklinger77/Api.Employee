using System.Collections.Generic;

namespace Api.ViewModels
{
    public class LoginResponsaViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel User { get; set; }
    }
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }

    }
    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
