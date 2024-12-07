using DataProvider.Interfaces.Users;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MansiaWebApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet("GetUserById")]  
        public IActionResult GetUserById(string Id)
        {
            if (Id.IsNullOrEmpty()) return BadRequest();

            var user = userRepository.GetUserById(Id);
            if (user != null)
            {
                return NotFound("User Not Found");
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { Data= user, Status = 200 });
            }
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users =  await userRepository.GetAllUsers();
            if (users != null)
            {
                return Ok(new { Data = users, Status = 200 });
            }
            return NotFound();
        }


    }
}
