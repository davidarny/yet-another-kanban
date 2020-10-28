using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace server
{
    public class AuthOptions
    {
        public const string ISSUER = "YakAuthServer"; // издатель токена
        public const string AUDIENCE = "YakAuthClient"; // потребитель токена
        const string KEY = "lwx3pigm9)fvtprvpcczf#^+2jx$5ey*(2hmw-@ndn41d7-sa*"; // ключ для шифрации
        public const int LIFETIME = 1 * 60 * 24; // время жизни токена - 24 часа

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
