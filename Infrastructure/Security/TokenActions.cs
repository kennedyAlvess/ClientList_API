
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClientListApi.Application.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ClientListApi.Security
{
    public interface ITokenActions
    {
        object GenerateToken(long vendedorId);
    }
    public class TokenActions : ITokenActions
    {
        private readonly JwtSettings _jwtSettings;

        public TokenActions(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public object GenerateToken(long vendedorId)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("UserId", vendedorId.ToString()),
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
