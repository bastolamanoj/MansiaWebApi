using DataProvider.DatabaseContext;
using DataProvider.Interfaces.Users;
using DataProvider.Models;
using Microsoft.Extensions.Logging;
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
        public RefreshTokenRepository(ApplicationDbContext context, ILogger<RefreshToken> logger):base(context, logger)
        {
            _logger = logger;   
        }

    }
}
