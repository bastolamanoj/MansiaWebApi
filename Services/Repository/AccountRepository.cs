using DataProvider.DTOs;
using DataProvider.DTOs.Login;
using DataProvider.DTOs.User;
using DataProvider.Interfaces;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static DataProvider.DTOs.ServiceResponses.ServiceResponse;
//using Microsoft.AspNetCore.mvc;

namespace Services.Repository
{
    public class AccountRepository : IAccountRepository
    {
        //private readonly signin<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager, RoleManager<Role> role, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = role;
            _configuration = configuration;
        }
        public async Task<GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is Empty");
            var newUser = new User()
            {
                Id= Guid.NewGuid(),
                FullName = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email,
                CreatedAt= DateTime.Now,
                UpdatedAt= DateTime.Now
            };
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user is not null) return new GeneralResponse(false, "User already exist..");

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);

            if (!createUser.Succeeded) return new GeneralResponse(false, "Error Occured, Please try again..");

            // Assign Default Role Admin to first registar; rest is user
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new Role() { Name = "Admin", AliasName = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created.");
            }
            else
            {
                var userRole = await _roleManager.FindByNameAsync("User");
                if (userRole is null)
                {
                    var createUserRole = await _roleManager.CreateAsync(new Role { Name = "User", AliasName = "User" });
                    if (!createUserRole.Succeeded)
                        return new GeneralResponse(false, "Failed to create User role.");
                }
                try
                {
                     await _userManager.AddToRoleAsync(newUser, "User");
                }catch(Exception ex)
                {
                    throw ex;
                }
            }
            return new GeneralResponse(true, "Account Created.");
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO model)
        {
            if (model is null) return new LoginResponse(false, "", "", "Login container is empty");

            var getUser = await _userManager.FindByEmailAsync(model.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "", "User not Found");

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, model.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "", "Invalid Password!!");

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            //var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole.First());
            string token = GenerateToken(getUser);
            return new LoginResponse(true, token!, "", "Login Success");
        }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var getUserRole = _userManager.GetRolesAsync(user);
            var userClaims = new[]
            {
                  new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim("UserId", user.Id.ToString()),
                  new Claim("Email", user.Email),
                  new Claim("Role", getUserRole.Result.First()),
            };

            var token = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
             audience: _configuration["Jwt:Audience"],
             claims: userClaims,    
             //expires: DateTime.Now.AddDays(1),
             expires: DateTime.UtcNow.AddMinutes( Int32.Parse(_configuration["Jwt:ExpirationInMinutes"])),
             signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
