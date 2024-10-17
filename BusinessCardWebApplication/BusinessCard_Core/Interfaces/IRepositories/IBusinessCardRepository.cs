using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Interfaces.GenericRepositoryInterface;
using BusinessCard_Core.Models.Entites;

namespace BusinessCard_Core.Interfaces.IRepositories
{
    public interface IBusinessCardRepository : IGenericRepository<BusinessCard>
    {
        Task<List<BusinessCardRecordDTO>> GetAllBusinessCardAsync();

    }
}
