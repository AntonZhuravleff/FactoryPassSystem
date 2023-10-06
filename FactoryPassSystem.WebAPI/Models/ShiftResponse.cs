using System;

namespace FactoryPassSystem.WebAPI.Models
{
    public class ShiftResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int HoursWorked { get; set; }    
    }
}
