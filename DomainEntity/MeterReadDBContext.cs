using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Ensek.Assignment.DomainEntity
{
    public class MeterReadDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterRead> MeterReads { get; set; }
    }
}
