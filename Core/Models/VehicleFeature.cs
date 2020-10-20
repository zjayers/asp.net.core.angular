using System.ComponentModel.DataAnnotations.Schema;

namespace asp.net.core.angular.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
