using ClientListApi.Data;
using ClientListApi.Dto;
using ClientListApi.Models;

namespace ClientListApi.Services
{
    public interface IClienteServices
    {
         Task<List<ClienteDto>> ListarClientes();
         Task<ClienteDto> BuscarClienteById(long id);
         Task<ClienteDto> RemoverCliente(long id);
         Task<ClienteDto> AtualizarCliente(ClienteDto cliente);
         Task<ClienteDto> AdicionarCliente(ClienteDto cliente);
    }

    public class ClienteServices : IClienteServices
    {
        private readonly AppDbContext _context;

        public ClienteServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteDto> AdicionarCliente(ClienteDto model)
        {
            try
            {
                ClienteModel cliente = new();
                cliente.Nome = model.Nome;
                cliente.Telefone = model.Telefone;
                cliente.Status = model.Status;
                cliente.DataNascimento = model.DataNascimento;
                cliente.Cep = model.Cep;
                cliente.Endereco = model.Endereco;
                cliente.NumeroEndereco = model.NumeroEndereco;
                cliente.Complementeo = model.Complementeo?? "";
                cliente.Bairro = model.Bairro;
                cliente.Cidade = model.Cidade;
                cliente.Estado = model.Estado;
                cliente.VendedorId = model.Id;

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException?.Message ?? "Erro ao adicionar cliente.");
            }
        }

        public async Task<ClienteDto> AtualizarCliente(ClienteDto model)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(model.Id);

                
                //_context.Update(cliente);
                //await _context.SaveChangesAsync();
                
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? "Erro ao atualizar cliente.");
            }
        }

        public Task<ClienteDto> BuscarClienteById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClienteDto>> ListarClientes()
        {
            throw new NotImplementedException();
        }

        public Task<ClienteDto> RemoverCliente(long id)
        {
            throw new NotImplementedException();
        }
    }
}