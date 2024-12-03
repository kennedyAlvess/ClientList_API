using ClientListApi.Models;

namespace ClientListApi.Dto
{
    public class ClienteDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = "";
        public string Telefone { get; set; } = "";
        public bool Status { get; set; }
        public DateOnly DataNascimento { get; set; }
        public long Cep { get; set; }
        public string Endereco { get; set; } = "";
        public string NumeroEndereco { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Cidade { get; set; } = "";
        public string Estado { get; set; } = "";
        public string? Complementeo { get; set; }
        public long VendedorId { get; set; }

        public ClienteDto ToClienteDTO(ClienteModel Cliente)
        {
            return new ClienteDto
            {
                Id = Cliente.Id,
                Nome = Cliente.Nome,
                Telefone = Cliente.Telefone,
                Status = Cliente.Status,
                DataNascimento = Cliente.DataNascimento,
                Cep = Cliente.Cep,
                Endereco = Cliente.Endereco,
                NumeroEndereco = Cliente.NumeroEndereco,
                Complementeo = Cliente.Complementeo ?? "",
                Bairro = Cliente.Bairro,
                Cidade = Cliente.Cidade,
                Estado = Cliente.Estado,
                VendedorId = this.VendedorId
            };
        }

        public ClienteModel ToClienteModel(ClienteDto Cliente)
        {
            return new ClienteModel
            {
                Id = Cliente.Id,
                Nome = Cliente.Nome,
                Telefone = Cliente.Telefone,
                Status = Cliente.Status,
                DataNascimento = Cliente.DataNascimento,
                Cep = Cliente.Cep,
                Endereco = Cliente.Endereco,
                NumeroEndereco = Cliente.NumeroEndereco,
                Complementeo = Cliente.Complementeo ?? "",
                Bairro = Cliente.Bairro,
                Cidade = Cliente.Cidade,
                Estado = Cliente.Estado,
                VendedorId = Cliente.VendedorId
            };
        }
    }


    public class ResponseDto
    {
        public required string Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
    }
}