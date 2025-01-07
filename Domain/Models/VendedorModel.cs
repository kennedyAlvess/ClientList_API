using System.Text.Json.Serialization;

namespace ClientListApi.Models
{
    public class VendedorModel
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public required string CPF { get; set; }
        public required bool Status { get; set; }
        [JsonIgnore]
        public ICollection<ClienteModel>? Clientes { get; set; }
    }
}