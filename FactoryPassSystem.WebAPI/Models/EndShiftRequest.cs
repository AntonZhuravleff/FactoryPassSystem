using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryPassSystem.WebAPI.Models
{
    public class EndShiftRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PassId { get; set; }
        [Required]
        public DateTime? EndTime { get; set; }
        public int HoursWorked { get; set; }
    }
}
