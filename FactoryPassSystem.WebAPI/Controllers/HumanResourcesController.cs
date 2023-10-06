using AutoMapper;
using FactoryPassSystem.WebAPI.Entities;
using FactoryPassSystem.WebAPI.Interfaces;
using FactoryPassSystem.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FactoryPassSystem.WebAPI.Controllers
{
    [ApiController]
    public class HumanResourcesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;

        public HumanResourcesController(IMapper mapper, IEmployeeRepository employeeRepository, IPositionRepository positionRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
        }

        [HttpGet("[controller]/Employees")]
        public async Task<ApiResult<List<EmployeeResponse>>> List(int? positionId, CancellationToken cancellationToken)
        {
            if (positionId.HasValue)
            {
                var position = await _positionRepository.GetByIdAsync(positionId.Value, cancellationToken);
                if (position == null)
                {
                    return BadRequest($"Position with Id={positionId} does not exist");
                }

                return _mapper.Map<List<EmployeeResponse>>(
                    await _employeeRepository.ListByPositionAsync(positionId.Value, cancellationToken));
            }
            else
            {
                return _mapper.Map<List<EmployeeResponse>>(
                    await _employeeRepository.ListWithPositionAndShiftsAsync(cancellationToken));
            }
        }

        [HttpGet("[controller]/Positions")]
        public async Task<ApiResult<List<PositionResponse>>> ListPositions(CancellationToken cancellationToken)
        {
            return _mapper.Map<List<PositionResponse>>(
                await _positionRepository.ListAsync(cancellationToken));
        }

        [HttpPost("[controller]/Employees")]
        public async Task<ApiResult<EmployeeResponse>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var position = await _positionRepository.GetByIdAsync(request.PositionId, cancellationToken);
            if (position == null)
            {
                return BadRequest($"Position with Id={request.PositionId} does not exist");
            }

            var newEmployee = await _employeeRepository.AddAsync(_mapper.Map<Employee>(request), cancellationToken);
            var newEmployeeWithPosition = await _employeeRepository.GetByIdWithPositionAsync(newEmployee.Id, cancellationToken);

            return _mapper.Map<EmployeeResponse>(newEmployeeWithPosition);
        }

        [HttpPut("[controller]/Employees")]
        public async Task<ApiResult<EmployeeResponse>> Update(UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (employee == null)
            {
                return BadRequest($"Employee with Id={request.Id} does not exist");
            }

            var position = await _positionRepository.GetByIdAsync(request.PositionId, cancellationToken);
            if (position == null)
            {
                return BadRequest($"Position with Id={request.PositionId} does not exist");
            }

            await _employeeRepository.UpdateAsync(_mapper.Map(request, employee), cancellationToken);

            return _mapper.Map<EmployeeResponse>(
                await _employeeRepository.GetByIdWithPositionAsync(request.Id, cancellationToken));
        }

        [HttpDelete("[controller]/Employees/{employeeId}")]
        public async Task<ApiResult> Delete([Required] int employeeId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employeeToDelete = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);
            if (employeeToDelete == null)
            {
                return BadRequest($"Employee with Id={employeeId} does not exist");
            }

            await _employeeRepository.DeleteAsync(employeeToDelete, cancellationToken);
            return Ok();
        }
    }
}
