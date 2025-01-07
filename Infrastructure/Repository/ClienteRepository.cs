using ClientListApi.Domain.Repositories;
using ClientListApi.Models;
using Microsoft.EntityFrameworkCore;


namespace ClientListApi.Infrastructure.Repository
{
    public class ClienteRepository(AppDbContext context) : IClienteRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<List<ClienteModel>> ListarClientes(long vendedorId)
        {
            try
            {
                return await _context.Clientes.Where(x => x.VendedorId == vendedorId)
                                                        .AsNoTracking()
                                                        .ToListAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao listar clientes.");
            }
        }
        public Task<string> AdicionarCliente(ClienteModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task<ClienteModel> AtualizarCliente(ClienteModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task<ClienteModel?> BuscarClienteById(long id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> RemoverCliente(long id)
        {
            throw new NotImplementedException();
        }
    }
}