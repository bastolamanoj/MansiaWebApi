using DataProvider.DatabaseContext;
using DataProvider.Interfaces;
using DataProvider.Models;
using Microsoft.Extensions.Logging;
using Services.Repository.Core;
using Services.Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ExceptionLoggerRepository : BaseRepository<ExceptionLogger>, IExceptionLoggerRepository
    {
        protected readonly ILogger<UserRepository> _logger;
        public ExceptionLoggerRepository(ApplicationDbContext dbcontext, ILogger<UserRepository> logger): base(dbcontext, logger) {
            _logger = logger;
        }  
    }
}
