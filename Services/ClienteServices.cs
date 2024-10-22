using ClientListApi.Data;
using ClientListApi.Dto;
using ClientListApi.Models;
using Microsoft.EntityFrameworkCore;

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
                ClienteModel cliente = new()
                {
                    Nome = model.Nome,
                    Telefone = model.Telefone,
                    Status = model.Status,
                    DataNascimento = model.DataNascimento,
                    Cep = model.Cep,
                    Endereco = model.Endereco,
                    NumeroEndereco = model.NumeroEndereco,
                    Complementeo = model.Complementeo ?? "",
                    Bairro = model.Bairro,
                    Cidade = model.Cidade,
                    Estado = model.Estado,
                    VendedorId = model.VendedorId
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar cliente.", ex);
            }
        }

        public async Task<ClienteDto> AtualizarCliente(ClienteDto model)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == model.Id)
                                                     .ExecuteUpdateAsync(set =>
                                                        set.SetProperty(x => x.Nome, model.Nome)
                                                        .SetProperty(x => x.Telefone, model.Telefone)
                                                        .SetProperty(x => x.Status, model.Status)
                                                        .SetProperty(x => x.DataNascimento, model.DataNascimento)
                                                        .SetProperty(x => x.Cep, model.Cep)
                                                        .SetProperty(x => x.Endereco, model.Endereco)
                                                        .SetProperty(x => x.NumeroEndereco, model.NumeroEndereco)
                                                        .SetProperty(x => x.Complementeo, model.Complementeo)
                                                        .SetProperty(x => x.Bairro, model.Bairro)
                                                        .SetProperty(x => x.Cidade, model.Cidade)
                                                        .SetProperty(x => x.Estado, model.Estado)
                                                        .SetProperty(x => x.VendedorId, model.VendedorId)
                                                     );

                return cliente == 0 ? throw new ArgumentException("Falha ao tentar atualizar dados do cliente.") 
                                    : await BuscarClienteById(model.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message?? "Erro ao atualizar cliente.", ex);
            }
        }

        public async Task<ClienteDto> BuscarClienteById(long id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == id)
                                               .Include(x => x.Vendedor)
                                               .FirstOrDefaultAsync() ?? throw new ArgumentException("Cliente não encontrado.");
                
                return new ClienteDto{
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Status = cliente.Status,
                    DataNascimento = cliente.DataNascimento,
                    Cep = cliente.Cep,
                    Endereco = cliente.Endereco,
                    NumeroEndereco = cliente.NumeroEndereco,
                    Complementeo = cliente.Complementeo?? "",
                    Bairro = cliente.Bairro,
                    Cidade = cliente.Cidade,
                    Estado = cliente.Estado,
                    Vendedor = cliente.Vendedor.Nome
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message?? "Erro ao buscar cliente.", ex);
            }
        }

        public async Task<List<ClienteDto>> ListarClientes()
        {
            try
            {
                var clientes = await _context.Clientes.Select(x => new ClienteDto
                {
                    Nome = x.Nome,
                    Telefone = x.Telefone,
                    Status = x.Status,
                    DataNascimento = x.DataNascimento,
                    Cep = x.Cep,
                    Endereco = x.Endereco,
                    NumeroEndereco = x.NumeroEndereco,
                    Complementeo = x.Complementeo?? "",
                    Bairro = x.Bairro,
                    Cidade = x.Cidade,
                    Estado = x.Estado,
                    Vendedor = x.Vendedor.Nome
                }).AsNoTracking().ToListAsync();

                if (clientes.Count == 0)
                    throw new ArgumentException("Nenhum cliente encontrado.");

                return clientes;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.InnerException?.Message?? "Erro ao listar clientes.", ex);
            }
        }

        public Task<ClienteDto> RemoverCliente(long id)
        {
            throw new NotImplementedException();
        }
    }
}