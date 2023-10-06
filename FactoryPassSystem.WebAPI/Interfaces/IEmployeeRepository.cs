using FactoryPassSystem.WebAPI.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Interfaces
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {    
        Task<Employee> GetByIdWithPositionAsync(int id, CancellationToken cancellationToken = default);
        Task<Employee> GetByPassIdAsync(int passId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Employee>> ListWithPositionAndShiftsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Employee>> ListByPositionAsync(int positionId, CancellationToken cancellationToken = default);
    }
}
