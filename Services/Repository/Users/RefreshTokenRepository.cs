using DataProvider.DatabaseContext;
using DataProvider.DTOs.User;
using DataProvider.Interfaces.Users;
using DataProvider.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Users
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken> ,IRefreshTokenRepository
    {
        private readonly ILogger<RefreshToken> _logger;
        protected readonly ApplicationDbContext _context;
        public RefreshTokenRepository(ApplicationDbContext context, ILogger<RefreshToken> logger):base(context, logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<RefreshTokenDTO> GetRefreshToken(string RefreshToken)
        {
           var token= await this._context.RefreshTokens.Include(r=>r.User).FirstOrDefaultAsync(t => t.Token == RefreshToken);
            if(token is not null)
            {
                return new RefreshTokenDTO() { 
                    Id= token.Id,
                    Token = token.Token,
                    ExpireOnUTC = token.ExpireOnUTC,   
                    user = token.User,
                    UserId= token.UserId
                };
            }
            return new RefreshTokenDTO();
        }
    }
}

