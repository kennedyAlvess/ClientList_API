using System.Reflection;
using ClientListApi.Application.Dto.InputDTO;
using ClientListApi.Application.Dto.ResponseDTO;
using ClientListApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ClientListApi.Application.Services
{
    public interface IClienteServices
    {
        Task<List<ResponseCliente>> ListarClientes(long vendedorId);
        Task<ResponseCliente?> BuscarClienteById(long id);
        Task<bool> RemoverCliente(long id);
        Task<ResponseCliente> AtualizarCliente(ClienteDto cliente);
        Task<string> AdicionarCliente(ClienteDto cliente);
    }

    public class ClienteServices(AppDbContext context) : IClienteServices
    {
        private readonly AppDbContext _context = context;
        readonly ClienteDto converterToModel = new();

        public async Task<List<ResponseCliente>> ListarClientes(long vendedorId)
        {
            try
            {
                var clientes = await _context.Clientes.Where(x => x.VendedorId == vendedorId)
                                                        .Select(x => new ResponseCliente(x))
                                                        .AsNoTracking()
                                                        .ToListAsync();

                return clientes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao listar clientes.");
            }
        }

        public async Task<ResponseCliente?> BuscarClienteById(long id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == id)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync();
                
                if (cliente is null)
                    return null;
                
                return new ResponseCliente(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao buscar cliente.");
            }
        }

        public async Task<string> AdicionarCliente(ClienteDto model)
        {
            try
            {
                _context.Clientes.Add(converterToModel.ToClienteModel(model));
                await _context.SaveChangesAsync();

                return "Cliente adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao adicionar cliente.");
            }
        }

        public async Task<ResponseCliente> AtualizarCliente(ClienteDto model)
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

        public async Task<bool> RemoverCliente(long id)
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