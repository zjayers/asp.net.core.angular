using System.ComponentModel.DataAnnotations;

namespace asp.net.core.angular.Controllers.Resources
{
    public class ContactInformationResource
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }
    }
}
