using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.DTOs.Login;
using DataProvider.Interfaces;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.mvc;

namespace Services.Repository
{
    public class AccountRepository :IAccountRepository
    {
        //private readonly signin<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountRepository(UserManager<User> userManager, RoleManager<Role> role)
        {
            _userManager= userManager;
            _roleManager= role;
        }

        Task<LoginDTO> IAccountRepository.Login(LoginDTO model)
        {
            throw new NotImplementedException();
        }

        //Task<LoginDTO> IAccountRepository.Login(LoginDTO model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
