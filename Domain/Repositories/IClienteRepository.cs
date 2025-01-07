using ClientListApi.Application.Dto.InputDTO;
using ClientListApi.Application.Dto.ResponseDTO;
using ClientListApi.Models;

namespace ClientListApi.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<List<ClienteModel>> ListarClientes(long vendedorId);
        Task<ClienteModel?> BuscarClienteById(long id);
        Task<bool> RemoverCliente(long id);
        Task<ClienteModel> AtualizarCliente(ClienteModel cliente);
        Task<string> AdicionarCliente(ClienteModel cliente);
    }
}