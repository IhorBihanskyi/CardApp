using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Atm.Heplers
{
    public class Constants
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 4000;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
             new(Encoding.ASCII.GetBytes(KEY));
    }
}
