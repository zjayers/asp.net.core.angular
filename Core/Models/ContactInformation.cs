using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace asp.net.core.angular.Models
{
    [Owned]
    public class ContactInformation
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }
    }
}
