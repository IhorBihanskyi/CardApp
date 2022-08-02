using Atm.Api.Heplers;
using Atm.Api.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Atm.Api.Services
{
    public class CardService : ICardService
    {
        public List<Card> GetCards()
        {
            using var stream = File.OpenText("cards.json");
            using var reader = new JsonTextReader(stream);
            return new JsonSerializer().Deserialize<List<Card>>(reader);
        }

        public string Authenticate(string password)
        {
            var card = GetCards().FirstOrDefault(x => x.Password == password);
            if (password != card.Password)
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, card.CardNumber),
                };

            claims.Add(new Claim(ClaimTypes.NameIdentifier, card.FullName));

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: Constants.ISSUER,
                audience: Constants.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(Constants.LIFETIME)),
                signingCredentials: new SigningCredentials(Constants.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
