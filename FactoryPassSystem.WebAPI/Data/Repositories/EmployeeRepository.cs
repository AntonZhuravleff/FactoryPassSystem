using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(FactoryPassDbContext dbContext) : base(dbContext) { }

        public async Task<Employee> GetByIdWithPositionAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Employee>()
                .Include(e => e.Position)
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Employee> GetByPassIdAsync(int passId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Employee>()
                .SingleOrDefaultAsync(e => e.PassId == passId, cancellationToken);
        }

        public async Task<IReadOnlyList<Employee>> ListByPositionAsync(int positionId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Employee>()
                .Where(e =>  e.Position != null && e.Position.Id == positionId)
                .Include(e => e.Position)
                .Include(e => e.Shifts)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Employee>> ListWithPositionAndShiftsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Employee>()
                .Include(e => e.Position)
                .Include(e => e.Shifts)
                .ToListAsync(cancellationToken);
        }
    }
}
