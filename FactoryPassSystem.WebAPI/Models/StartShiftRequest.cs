using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryPassSystem.WebAPI.Models
{
    public class StartShiftRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int PassId { get; set; }
        [Required]
        public DateTime? StartTime { get; set; }
    }
}
