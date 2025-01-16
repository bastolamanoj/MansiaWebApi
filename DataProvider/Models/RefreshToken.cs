using DataProvider.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get;set; }
        public string UserId { get; set; } 
        public DateTime ExpireOnUTC { get; set; }   
        public User User { get; set; }
    }
}
