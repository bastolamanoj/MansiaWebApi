using DataProvider.DTOs.Login;
using DataProvider.DTOs.User;
using DataProvider.Interfaces;
using DataProvider.Interfaces.Users;
using DataProvider.Models;
using DataProvider.Models.Identity;
using MansiaWebApi.Infrastructure;
using MansiaWebApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static DataProvider.DTOs.ServiceResponses.ServiceResponse;

namespace MansiaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
       
        public AccountController(IAccountRepository accountRepository, IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository) {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            var response = await _accountRepository.CreateAccount(userDto);
            if (response.Flag)
            {
             return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _accountRepository.LoginAccount(loginDTO);
            var refreshTokenValue = "";
            if (response.Flag) { 
              TokenProvider _tokenProvider = new TokenProvider();
               UserDTO? user = await _userRepository.GetUserByEmail(loginDTO.Email);
                var refreshToken = new RefreshToken() {
                    Id = Guid.NewGuid(),
                    Token = _tokenProvider.GenerateRefreshToken(),
                    UserId = user.Id,
                    ExpireOnUTC = DateTime.UtcNow.AddDays(7)
                };

                _refreshTokenRepository.Add(refreshToken);
                refreshTokenValue = refreshToken.Token;
            }
            return Ok(new LoginResponse(true,response.Token, refreshTokenValue, response.Message));
        }

       
    }
}
