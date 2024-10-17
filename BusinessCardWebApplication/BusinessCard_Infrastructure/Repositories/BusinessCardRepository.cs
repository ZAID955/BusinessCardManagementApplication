using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Interfaces.IRepositories;
using BusinessCard_Core.Models.Entites;
using BusinessCard_Infrastructure.DBContext;
using BusinessCard_Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard_Infrastructure.Repositories
{
    public class BusinessCardRepository : GenericRepository<BusinessCard>, IBusinessCardRepository
    {
        public BusinessCardRepository(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<List<BusinessCardRecordDTO>> GetAllBusinessCardAsync()
        {
            var result = from businessCard in _context.BusinessCards
                         select new BusinessCardRecordDTO
                         {
                             Id = businessCard.Id,
                             Name = businessCard.Name,
                             Gendear = businessCard.Gendear.ToString(),
                             DateOfBirth = businessCard.DateOfBirth.ToString(),
                             Email = businessCard.Email,
                             Phone = businessCard.Phone,
                             Photo = businessCard.PhotoPath,
                             Address = businessCard.Address,
                         };
            return await result.ToListAsync();
        }

    }
}
