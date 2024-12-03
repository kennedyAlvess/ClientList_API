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
        Task<ResponseDto> AdicionarCliente(ClienteDto cliente);
    }

    public class ClienteServices(AppDbContext context) : IClienteServices
    {
        private readonly AppDbContext _context = context;
        readonly ClienteDto converterToDTO = new();
        readonly ClienteDto converterToModel = new();

        public async Task<List<ClienteDto>> ListarClientes(long vendedorId)
        {
            try
            {
                var clientes = await _context.Clientes.Where(x => x.VendedorId == vendedorId)
                                                        .Select(cliente => converterToDTO.ToClienteDTO(cliente))
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

        public async Task<ClienteDto?> BuscarClienteById(long id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Id == id)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync();
                
                if (cliente is null)
                    return null;
                
                return converterToDTO.ToClienteDTO(cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao buscar cliente.");
            }
        }

        public async Task<ResponseDto> AdicionarCliente(ClienteDto model)
        {
            try
            {
                List<string> Erros = await ValidarCliente(model);

                if(Erros.Count > 0)
                    return new ResponseDto{
                        Message = "Dados Invalidos.",
                        Errors = Erros
                    };

                _context.Clientes.Add(converterToModel.ToClienteModel(model));
                await _context.SaveChangesAsync();

                return new ResponseDto
                {
                    Message = "Cliente adicionado com sucesso."
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //Medida provisoria para logs
                throw new Exception("Erro ao adicionar cliente.");
            }
        }

        private async static Task<List<string>> ValidarCliente(ClienteDto model)
        {
            var Erros = new List<String>();

            if (string.IsNullOrEmpty(model.Nome))
                Erros.Add("Nome é obrigatório.");

            if (string.IsNullOrEmpty(model.Telefone))
                Erros.Add("Telefone é obrigatório.");

            if (string.IsNullOrEmpty(model.DataNascimento.ToString()))
                Erros.Add("Data de nascimento é obrigatória.");

            if (model.Cep.ToString().Length != 8)
                Erros.Add("CEP é obrigatório.");

            if (string.IsNullOrEmpty(model.Endereco))
                Erros.Add("Endereço é obrigatório.");

            if (string.IsNullOrEmpty(model.NumeroEndereco))
                Erros.Add("Número do endereço é obrigatório.");

            if (string.IsNullOrEmpty(model.Bairro))
                Erros.Add("Bairro é obrigatório.");

            if (string.IsNullOrEmpty(model.Cidade))
                Erros.Add("Cidade é obrigatória.");

            if (string.IsNullOrEmpty(model.Estado))
                Erros.Add("Estado é obrigatório.");

            return Erros;
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