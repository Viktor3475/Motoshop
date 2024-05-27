using ApplicationData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class OrderDTO:BaseDTO
    {

        public int MotorcycleId { get; set; }
        public virtual Motorcycle? Motorcycle { get; set; }

        public int UserId { get; set; }

        public virtual User? User { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string OrderName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
