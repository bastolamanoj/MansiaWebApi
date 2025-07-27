using DataProvider.DTOs.Login;
using DataProvider.DTOs.User;
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

    }
}
