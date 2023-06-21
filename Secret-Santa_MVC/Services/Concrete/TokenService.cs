using Microsoft.AspNetCore.Identity;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Extensions;
using Secret_Santa_MVC.Services.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace Secret_Santa_MVC.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(ApplicationUser user, List<IdentityRole<long>> roles)
        {
            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(_configuration);
            var tokenHadler = new JwtSecurityTokenHandler();

            return tokenHadler.WriteToken(token);
        }
    }
}
