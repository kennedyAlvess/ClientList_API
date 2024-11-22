using ClientListApi.Models;

namespace ClientListApi.Dto
{
    public class VendedorDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public bool Status { get; set; }
        public List<ClienteModel>? Clientes { get; set; }
    }
}