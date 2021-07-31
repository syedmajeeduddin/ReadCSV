using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Models
{
    public class Account
    {

        [Required]
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage ="First Name can not be more than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(80, ErrorMessage = "Last Name can not be more than 50 characters")]
        public string LastName { get; set; }
    }
}
