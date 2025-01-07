using ClientListApi.Models;

namespace ClientListApi.Application.Dto.ResponseDTO
{
    public class ResponseCliente(ClienteModel model)
    {
        public string Nome { get; set; } = model.Nome;
        public string Telefone { get; set; } = model.Telefone;
        public bool Status { get; set; } = model.Status;
        public string StatusCliente => Status ? "Ativo" : "Inativo";
        public DateOnly DataNascimento { get; set; } = model.DataNascimento;
        public long Cep { get; set; } = model.Cep;
        public string Endereco { get; set; } = model.Endereco;
        public string NumeroEndereco { get; set; } = model.NumeroEndereco;
        public string Bairro { get; set; } = model.Bairro;
        public string Cidade { get; set; } = model.Cidade;
        public string Estado { get; set; } = model.Estado;
        public string? Complementeo { get; set; } = model.Complementeo;
    }


}