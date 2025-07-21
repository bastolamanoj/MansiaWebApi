using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.DTOs.User
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpireOnUTC { get; set; }
        public DataProvider.Models.Identity.User user { get; set; }  
    }
}
