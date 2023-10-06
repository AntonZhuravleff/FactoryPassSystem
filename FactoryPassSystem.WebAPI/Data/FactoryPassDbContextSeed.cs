using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FactoryPassSystem.WebAPI.Entities;
using System.Collections.Generic;

namespace FactoryPassSystem.WebAPI.Data
{
    public static class FactoryPassDbContextSeed
    {
        public static async Task SeedAsync(FactoryPassDbContext dbContext, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (!await dbContext.Positions.AnyAsync())
                {
                    await dbContext.Positions.AddRangeAsync(
                        GetPreconfiguredPositions());

                    await dbContext.SaveChangesAsync();
                }
            }
            catch 
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                await SeedAsync(dbContext, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Position> GetPreconfiguredPositions()
        {
            return new List<Position>
            {
                new("Менеджер"),
                new("Инженер"),
                new("Тестировщик свечей"),
            };
        }
    }
}
