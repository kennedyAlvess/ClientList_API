using ClientListApi.Data;
using ClientListApi.Dto;
using ClientListApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientListApi.Services
{
    public interface IClienteServices
    {
        Task<List<ClienteDto>> ListarClientes(long vendedorId);
        Task<ClienteDto?> BuscarClienteById(long id);
        Task<Boolean> RemoverCliente(long id);
        Task<ClienteDto> AtualizarCliente(ClienteDto cliente);
        Task<ClienteDto> AdicionarCliente(ClienteDto cliente);
    }

    public class ClienteServices(AppDbContext context) : IClienteServices
    {
        private readonly AppDbContext _context = context;
        readonly ClienteDto converter = new();

        public async Task<List<ClienteDto>> ListarClientes(long vendedorId)
        {
            try
            {
                var clientes = await _context.Clientes.Where(x => x.VendedorId == vendedorId)
                                                        .Select(cliente => converter.ToClienteDTO(cliente))
                                                        .AsNoTracking()
                                                        .ToListAsync();

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes.", ex);
            }
        }

        public async Task<ClienteDto?> BuscarClienteById(long id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == id)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync();
                
                if (cliente is null)
                    return null;
                
                return converter.ToClienteDTO(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar Cliente", ex);
            }
        }

        public async Task<ClienteDto> AtualizarCliente(ClienteDto model)
        {
            try
            {
                var cliente = await BuscarClienteById(model.Id) ?? throw new ArgumentException("Cliente não encontrado.");

                int atualizarCliente = await _context.Clientes.Where(x => x.Id == model.Id)
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
                                                     );

                return await BuscarClienteById(model.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? "Erro ao atualizar cliente.", ex);
            }
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
                throw new Exception("Erro ao adicionar cliente." + ex.Message, ex);
            }
        }

        public async Task<Boolean> RemoverCliente(long id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == id)
                                                  .FirstOrDefaultAsync() ?? throw new ArgumentException("Cliente não encontrado.");

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? "Erro ao remover cliente.", ex);
            }
        }
    }
}