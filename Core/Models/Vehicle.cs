using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using asp.net.core.angular.Core.Models;

namespace asp.net.core.angular.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public bool IsRegistered { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public DateTime LastUpdate { get; set; }

        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }
}
