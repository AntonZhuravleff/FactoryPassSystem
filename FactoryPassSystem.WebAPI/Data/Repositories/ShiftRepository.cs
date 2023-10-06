using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Data.Repositories
{
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
    {
        public ShiftRepository(FactoryPassDbContext dbContext) : base(dbContext) { }

        public async Task<Shift> GetLastShiftAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Shift>()
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
