using FactoryPassSystem.WebAPI.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Interfaces
{
    public interface IShiftRepository : IRepositoryBase<Shift>
    {
        Task<Shift> GetLastShiftAsync(int employeeId, CancellationToken cancellationToken = default);
    }
}
