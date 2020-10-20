using System.Threading.Tasks;

namespace asp.net.core.angular.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
