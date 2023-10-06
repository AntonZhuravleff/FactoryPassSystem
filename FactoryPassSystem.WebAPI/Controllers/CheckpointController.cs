using FactoryPassSystem.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using FactoryPassSystem.WebAPI.Interfaces;
using System;

namespace FactoryPassSystem.WebAPI.Controllers
{
    [ApiController]
    public class CheckpointController : ControllerBase
    {
        private readonly ICheckpointService _checkpointService;
        public CheckpointController(ICheckpointService checkpointService)
        {
            _checkpointService = checkpointService;
        }

        [HttpPost("[controller]/Start")]
        public async Task<ApiResult> StartShift(StartShiftRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _checkpointService.StartShiftAsync(request.PassId, request.StartTime.Value, cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("[controller]/End")]
        public async Task<ApiResult> EndShift(EndShiftRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _checkpointService.EndShiftAsync(request.PassId, request.EndTime.Value, request.HoursWorked, cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
