using BusinessCard_Core.Interfaces.IRepositories;
using BusinessCard_Core.Interfaces.UnitOfWorkInterface;
using BusinessCard_Infrastructure.DBContext;

namespace BusinessCard_Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IBusinessCardRepository BusinessCards { get; }

        public UnitOfWork(ApplicationDBContext context,
                          IBusinessCardRepository businessCard)
        {
            _context = context;
            BusinessCards = businessCard;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
