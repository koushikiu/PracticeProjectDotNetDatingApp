using DataModel.Entites;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class TockenService : ITockenService
    {

        private readonly SymmetricSecurityKey _key;
        public TockenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateTocken(AppUser user)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tockenDercriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tockenHanlder = new JwtSecurityTokenHandler();
            var tocken = tockenHanlder.CreateToken(tockenDercriptor);
            return tockenHanlder.WriteToken(tocken);
        }
    }
}
public interface ITockenService
{
    string CreateTocken(AppUser user);
}
