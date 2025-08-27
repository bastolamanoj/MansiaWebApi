using DataProvider.DTOs.Account;
using DataProvider.DTOs.Login;
using DataProvider.DTOs.User;
using DataProvider.Interfaces;
using DataProvider.Interfaces.Users;
using DataProvider.Models;
using DataProvider.Models.Identity;
using MansiaWebApi.Infrastructure;
using Microsoft.AspNetCore.Identity;
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
        private readonly TokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;    
       
        public AccountController(IAccountRepository accountRepository, IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository, TokenProvider tokenProvider,UserManager<User> userManager) {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenProvider = tokenProvider;
            _userManager = userManager; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            try
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
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message.ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _accountRepository.LoginAccount(loginDTO);
            var refreshTokenValue = "";
            if (response.Flag) { 
              //TokenProvider _tokenProvider = new TokenProvider();
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string RefreshToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshToken(RefreshToken);
            if(refreshToken is null || refreshToken.ExpireOnUTC < DateTime.UtcNow)
            {
                throw new ApplicationException("The Refresh Token has expired.");
            }
            string accessToken = _tokenProvider.GenerateAccessToken(refreshToken.user);

            refreshToken.Token = _tokenProvider.GenerateRefreshToken();
            refreshToken.ExpireOnUTC = DateTime.UtcNow.AddDays(1);
            _refreshTokenRepository.Add(new RefreshToken() { Token=refreshToken.Token, ExpireOnUTC = refreshToken.ExpireOnUTC,UserId=refreshToken.UserId});
            //this will update the existing refresh token in the database
            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken.Token });


        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
        {
            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "New password and confirmation password do not match."
                });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "User is not logged in or no user found."
                });
            }

            var result = await _accountRepository.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Password change failed.",
                    errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            return Ok(new
            {
                success = true,
                message = "Password changed successfully."
            });
        }


    }

}
