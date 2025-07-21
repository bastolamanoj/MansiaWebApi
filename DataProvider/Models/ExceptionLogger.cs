using DataProvider.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProvider.Models
{
    public class ExceptionLogger
    {
        [Key]
        public Guid Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]    
        public User User { get; set; }  
    }
}
