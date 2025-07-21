using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public string AliasName { get; set; }
    }
}
