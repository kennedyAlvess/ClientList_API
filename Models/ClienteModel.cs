using System.Text.Json.Serialization;

namespace ClientListApi.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public bool Status { get; set; }
        public DateOnly DataNascimento { get; set; }
        public long Cep { get; set;}
        public required string Endereco { get; set; }
        public required string NumeroEndereco { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set;} 
        public required string Estado { get; set;} 
        public string? Complementeo { get; set; }
        public long VendedorId { get; set; }
        [JsonIgnore]
        public VendedorModel Vendedor { get; set; } = null!;
    }
}