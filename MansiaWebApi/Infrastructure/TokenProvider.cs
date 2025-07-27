using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MansiaWebApi.Infrastructure
{
    public class TokenProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public TokenProvider(UserManager<User> userManager, RoleManager<Role> role, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = role;
            _configuration = configuration;
        }
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));  
        }

        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var getUserRole = _userManager.GetRolesAsync(user);
            var userClaims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim("userId", user.Id.ToString()),
                  new Claim("fullName", user.FullName),
                  new Claim("email", user.Email),
                  new Claim("role", getUserRole.Result.First()),
            };

            var token = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
             audience: _configuration["Jwt:Audience"],
             claims: userClaims,
             //expires: DateTime.Now.AddDays(1),
             expires: DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:ExpirationInMinutes"])),
             signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
