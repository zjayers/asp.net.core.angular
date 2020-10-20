using System.Threading.Tasks;
using asp.net.core.angular.Models;

namespace asp.net.core.angular.Persistence
{
    public interface IVehicleRepository
    {

        Task<Vehicle> GetVehicle(int id, bool includeRelatedData = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
