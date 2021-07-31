using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeterReadingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingAPI.Repository
{
    public class MeterReadRepository : GenericRepository<MeterRead>, IMeterReadRepository
    {
        public MeterReadRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
