using MeterReadingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            MeterReads = new MeterReadRepository(_context);
            Accounts = new AccountRepository(_context);
        }

        public IMeterReadRepository MeterReads { get; private set; }
        public IAccountRepository Accounts { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
