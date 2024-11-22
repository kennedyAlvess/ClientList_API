using ClientListApi.Data;
using ClientListApi.Dto;
using ClientListApi.Security;
using Microsoft.EntityFrameworkCore;

namespace ClientListApi.Services
{
    public interface ILoginServices
    {
        Task<object> Login(LoginDto user);
    }
    class LoginServices : ILoginServices
    {
        private readonly AppDbContext _context;
        private readonly ITokenActions _tokenActions;
        public LoginServices(AppDbContext context, ITokenActions tokenActions)
        {
            _context = context;
            _tokenActions = tokenActions;
        }
        public async Task<object> Login(LoginDto user)
        {
            var vendedor = await _context.Vendedores.Where(x => x.Nome == user.Nome && x.Senha == user.Senha)
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync() 
                                                ?? throw new Exception("Usuário ou senha inválidos");
            
            return _tokenActions.GenerateToken(vendedor.Id);
        }
    }
}