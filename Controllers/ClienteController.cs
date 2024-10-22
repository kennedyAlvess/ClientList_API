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

        [HttpPut("AtualizarCliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteDto cliente)
        {
            try
            {
                return Ok(await _clienteServices.AtualizarCliente(cliente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ClienteById/{id}")]
        public async Task<IActionResult> BuscarClienteById(long id)
        {
            try
            {
                return Ok(await _clienteServices.BuscarClienteById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            try
            {
                return Ok(await _clienteServices.ListarClientes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}