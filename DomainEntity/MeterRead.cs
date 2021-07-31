using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ensek.Assignment.DomainEntity
{
    public class MeterRead
    {
       
         [Key]
        public int AccountId { get; set; }
        public DateTime MeterReadDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
