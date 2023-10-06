using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Services
{
    public class CheckpointService : ICheckpointService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CheckpointService(IShiftRepository shiftRepository, IEmployeeRepository employeeRepository)
        {
            _shiftRepository = shiftRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task StartShiftAsync(int passId, DateTime startTime, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByPassIdAsync(passId, cancellationToken);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with passId={passId} does not exist");
            }

            // Employee can't start new shift if his last shift is unfinished
            var lastShift = await _shiftRepository.GetLastShiftAsync(employee.Id, cancellationToken);
            if (lastShift != null && !lastShift.EndTime.HasValue)
            {
                throw new CheckpointException($"Cannot start a new shift while last shift is unfinished");
            }
     
            employee.Shifts.Add(Shift.StartNew(employee.Id, startTime));
            await _employeeRepository.UpdateAsync(employee, cancellationToken);
        }

        public async Task EndShiftAsync(int passId, DateTime endTime, int hoursWorked, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByPassIdAsync(passId, cancellationToken);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with passId={passId} does not exist");
            }

            // Employee can't end unstarted shift or ended shift
            var lastShift = await _shiftRepository.GetLastShiftAsync(employee.Id, cancellationToken);
            if (lastShift == null || lastShift.EndTime.HasValue)
            {
                throw new CheckpointException($"Cannot end an unstarted shift");
            }

            lastShift.EndShift(endTime, hoursWorked);

            await _shiftRepository.UpdateAsync(lastShift, cancellationToken);
        }
    }
}
