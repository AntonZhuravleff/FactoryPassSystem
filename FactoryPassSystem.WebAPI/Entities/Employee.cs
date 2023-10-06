using System.Collections.Generic;

namespace FactoryPassSystem.WebAPI.Entities
{
    public class Employee : BaseEntity
    {
        public int? PassId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
