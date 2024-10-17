using BusinessCard_Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Core.Interfaces.UnitOfWorkInterface
{
    public interface IUnitOfWork : IDisposable
    {
        IBusinessCardRepository BusinessCards { get; }
        Task<int> SaveAsync();

    }
}
