using ClientListApi.Dto;
using ClientListApi.Security;
using ClientListApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientListApi.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices, ITokenActions tokenActions)
        {
            _loginServices = loginServices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {
                return Ok(await _loginServices.Login(user));
            }
            catch (Exception ex)
            {
                if (ex.Message == "Usuário ou senha inválidos")
                    return Unauthorized(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
        
    }
}