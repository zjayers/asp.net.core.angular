using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.net.core.angular.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required] [MaxLength(255)] public string Name { get; set; }
    }
}
