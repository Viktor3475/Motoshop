using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class MotorcycleDTO:BaseDTO
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Make { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }

        [Range(25, 320)]
        public int HP { get; set; }

        public bool IsAvailable { get; set; }

    }
}
