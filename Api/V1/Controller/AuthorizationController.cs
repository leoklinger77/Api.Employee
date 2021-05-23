using Api.Controllers;
using Api.Extension;
using Api.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.V1.Controller
{
    [Route("api/v1/Authentication")]
    public class AuthorizationController : MainController
    {
        private readonly IdentityTokenManipulation _identityToken;
        public AuthorizationController(INotifier notifier, IMapper mapper, IdentityTokenManipulation identityToken) : base(notifier, mapper)
        {
            _identityToken = identityToken;
        }
        
        [HttpPost]
        public async Task<ActionResult<LoginResponsaViewModel>> Register(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);
            
            if(await _identityToken.FirstAccess(loginUser.Email, loginUser.Password))
            {
                return CustomResponse(await _identityToken.GeneratorToken(loginUser.Email));
            }
            else
            {
                return CustomResponse();
            }
        }
    }
}
