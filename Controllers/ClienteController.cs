using ClientListApi.Dto;
using ClientListApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientListApi.Controllers
{
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }   

        [HttpPost("AdicionarCliente")]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDto cliente)
        {
            try
            {
                return Ok(await _clienteServices.AdicionarCliente(cliente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}