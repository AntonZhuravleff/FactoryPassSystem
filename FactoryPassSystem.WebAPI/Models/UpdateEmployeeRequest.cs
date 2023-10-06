using System.ComponentModel.DataAnnotations;

namespace FactoryPassSystem.WebAPI.Models
{
    public class UpdateEmployeeRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        public int? PassId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PositionId { get; set; }
    }
}
