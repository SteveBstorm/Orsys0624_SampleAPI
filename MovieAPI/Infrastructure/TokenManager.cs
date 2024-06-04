using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MovieAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieAPI.Infrastructure
{
    public class TokenManager
    {
        public static string _secretkey = "oiuare^navu rzeaiur vapiur^naezaaaaaaaaaaaasssssssaaaaaaaaaaaiv uaezàç'!i!àçkjazpir ";

        public string GenerateToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretkey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Role, user.IsAdmin ? "admin":"user"),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: myClaims,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
    }
}
