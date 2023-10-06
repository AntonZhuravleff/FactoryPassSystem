using System.Collections.Generic;

namespace FactoryPassSystem.WebAPI.Models
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public int? PassId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public PositionResponse Position { get; set; }
        public List<ShiftResponse> Shifts { get; set; } = new List<ShiftResponse>();
    }
}
