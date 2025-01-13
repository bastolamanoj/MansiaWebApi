using System.Buffers.Text;
using System.Security.Cryptography;

namespace MansiaWebApi.Infrastructure
{
    public class TokenProvider
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));  
        }
    }
}
