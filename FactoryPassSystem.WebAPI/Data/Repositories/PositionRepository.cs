using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Interfaces;

namespace FactoryPassSystem.WebAPI.Data.Repositories
{
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(FactoryPassDbContext dbContext) : base(dbContext) { }
    }
}
