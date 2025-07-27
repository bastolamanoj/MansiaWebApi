using DataProvider.DTOs.User;
using DataProvider.Interfaces.Core;
using DataProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces.Users
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshTokenDTO> GetRefreshToken(string RefreshToken);
    }
}
