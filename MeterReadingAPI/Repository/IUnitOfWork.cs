using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IMeterReadRepository MeterReads { get; }
        IAccountRepository Accounts { get; }
        int Complete();
    }
}
