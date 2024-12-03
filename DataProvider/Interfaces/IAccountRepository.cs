using DataProvider.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface IAccountRepository
    {
        //Task<IActionResult> a
       public Task<LoginDTO> Login(LoginDTO model);
    }
}
