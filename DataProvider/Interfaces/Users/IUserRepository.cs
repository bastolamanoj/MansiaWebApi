using DataProvider.DTOs.User;
using DataProvider.Interfaces.Core;
using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserDTO> GetUserById(string id);  
        Task<List<UserDTO>> GetAllUsers();  
    }
}
