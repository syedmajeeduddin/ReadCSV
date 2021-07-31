using System;
using System.ComponentModel.DataAnnotations;

namespace Ensek.Assignment.DomainEntity
{
    public class Account
    {

        [Key]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
