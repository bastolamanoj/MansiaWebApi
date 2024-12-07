using DataProvider.DatabaseContext;
using DataProvider.DTOs.User;
using DataProvider.Interfaces.Users;
using DataProvider.Models.Identity;
using Microsoft.Extensions.Logging;
using Services.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger)
             : base(dbContext, logger)
        {
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
           var users = await this.GetAllAsync();
            List<UserDTO> userList = new List<UserDTO>(); 
            foreach (var item in users)
            {
                userList.Add(new UserDTO
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    Id = item.Id,
                    Name = item.FullName
                });
                
            }
            return userList;
        }

        public async Task<UserDTO> GetUserById(string Id)
        {
            var user = await this.GetById(Id);
            var userDTO = new UserDTO() { 
                Id=user.Id,
                Name=user.UserName,
                Email=user.Email 
            };

            return userDTO;
        }
    }
}
