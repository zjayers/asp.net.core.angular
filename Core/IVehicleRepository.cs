using System.Collections.Generic;
using System.Threading.Tasks;
using asp.net.core.angular.Core.Models;
using asp.net.core.angular.Models;

namespace asp.net.core.angular.Core
{
    public interface IVehicleRepository
    {
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery vehicleQuery);
        Task<Vehicle> GetVehicle(int id, bool includeRelatedData = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
