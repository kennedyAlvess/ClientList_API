using System.Text.Json.Serialization;

namespace ClientListApi.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }
        public  string Nome { get; set; }
        public  string Telefone { get; set; }
        public  bool Status { get; set; }
        public  DateOnly DataNascimento { get; set; }
        public  long Cep { get; set;}
        public  string Endereco { get; set; }
        public  string NumeroEndereco { get; set; }
        public string? Complementeo { get; set;}
        public  string Bairro { get; set; }
        public  string Cidade { get; set;}
        public  string Estado { get; set;}
        public long VendedorId { get; set; }
        [JsonIgnore]
        public virtual VendedorModel Vendedor { get; set; }
    }
}