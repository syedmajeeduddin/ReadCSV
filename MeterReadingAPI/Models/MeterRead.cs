using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Models
{
    public class MeterRead
    {
        [Key]
        [Required]
        public int MeterReadId { get; set; }

        [Required]        
        public int AccountId { get; set; }


        public DateTime MeterReadDateTime { get; set; }


        public int MeterReadValue { get; set; }
    }
}
