using System;

namespace FactoryPassSystem.WebAPI.Entities
{
    public class Shift : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int HoursWorked { get; set; }

        private Shift() { }
        private Shift(int employeeId, DateTime startTime)
        {
            EmployeeId = employeeId;
            StartTime = startTime;
        }

        public static Shift StartNew(int employeeId, DateTime startTime) => new Shift(employeeId, startTime);

        public void EndShift(DateTime endTime, int hoursWorked)
        {
            EndTime = endTime;
            HoursWorked = hoursWorked;
        }
    }
}
