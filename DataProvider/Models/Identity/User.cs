using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Models.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string? DisplayName { get; set; }
        [MaxLength(500, ErrorMessage = "Your Bio must be at most 500 characters")]
        public string? FirstName { get;set; }
        public string? LastName { get;set; }
        public string? FullName { get;set; }
        public string? Bio { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public string? Country { get; set; }
        public string? ProfileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
