using System.Collections.Generic;
using System.Threading.Tasks;
using asp.net.core.angular.Core.Models;

namespace asp.net.core.angular.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}
