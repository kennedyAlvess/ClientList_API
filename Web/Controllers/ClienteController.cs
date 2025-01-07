using ClientListApi.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientListApi.Controllers
{
    [Route("api/")]
    [ApiController]
    //[Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }


        [HttpGet("ListarClientes")]
        public async Task<IActionResult> ListarClientes(long vendedorId)
        {
            try
            {
                var clientes = await _clienteServices.ListarClientes(vendedorId);
                if (clientes.Count == 0)
                    return NotFound(new
                    {
                        message = "Não há clientes cadastrados para o vendedor informado."
                    });

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet("ClienteById/{id}")]
        // public async Task<IActionResult> BuscarClienteById(long id)
        // {
        //     try
        //     {
        //         var cliente = await _clienteServices.BuscarClienteById(id);

        //         if (cliente is null)
        //             return NotFound(new 
        //             {
        //                 message = "Cliente não encontrado."
        //             });

        //         return Ok(cliente);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // [HttpPost("AdicionarCliente")]
        // public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDto cliente)
        // {
        //     try
        //     {
        //         var adicionarCliente = await _clienteServices.AdicionarCliente(cliente);
        //         if (adicionarCliente.Errors.Any())
        //             return BadRequest(adicionarCliente);

        //         return Ok(adicionarCliente);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // [HttpPut("AtualizarCliente")]
        // public async Task<IActionResult> AtualizarCliente([FromBody] ClienteDto cliente)
        // {
        //     try
        //     {
        //         return Ok(await _clienteServices.AtualizarCliente(cliente));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // [HttpDelete("RemoverCliente/{id}")]
        // public async Task<IActionResult> RemoverCliente(long id)
        // {
        //     try
        //     {
        //         return Ok(await _clienteServices.RemoverCliente(id) is true ? "Cliente removido com sucesso." : "Cliente não encontrado.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}