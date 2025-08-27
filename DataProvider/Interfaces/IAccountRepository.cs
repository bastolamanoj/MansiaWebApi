using DataProvider.DTOs.Login;
using DataProvider.DTOs.User;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataProvider.DTOs.ServiceResponses.ServiceResponse;

namespace DataProvider.Interfaces
{
    public interface IAccountRepository
    {
        //Task<IActionResult> a
        Task<LoginResponse> LoginAccount(LoginDTO model);
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);


    }
}
