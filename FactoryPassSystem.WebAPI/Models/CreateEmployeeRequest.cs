using System.ComponentModel.DataAnnotations;

namespace FactoryPassSystem.WebAPI.Models
{
    public class CreateEmployeeRequest
    {
        public int? PassId { get; set; } 
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int PositionId { get; set; }
    }
}
