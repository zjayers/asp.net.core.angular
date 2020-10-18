using System.Collections.Generic;

namespace asp.net.core.angular.wwwroot
{
    public class MakeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelResource> Models { get; set; }
    }
}
