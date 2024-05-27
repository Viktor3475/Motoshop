using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationData.Data
{
    public class User:BaseEntity
    {
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string FName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 4)]
        public string LName { get; set; }
        [RegularExpression("M|F")]
        public char Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

    }
}
