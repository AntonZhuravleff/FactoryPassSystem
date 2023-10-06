using System;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Interfaces
{
    public interface ICheckpointService
    {
        Task StartShiftAsync(int passId, DateTime startTime, CancellationToken cancellationToken = default);
        Task EndShiftAsync(int passId, DateTime endTime, int hoursWorked, CancellationToken cancellationToken = default);
    }
}
