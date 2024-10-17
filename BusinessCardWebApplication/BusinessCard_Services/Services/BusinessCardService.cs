using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Interfaces.UnitOfWorkInterface;
using BusinessCard_Core.Models.Entites;
using BusinessCard_Services.IServices;
using static BusinessCard_Core.Helpers.Enums.ApplicationLookups;


namespace BusinessCard_Services.Services
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessCardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BusinessCardRecordDTO>> GetAllBusinessCardAsync()
        {
            return await _unitOfWork.BusinessCards.GetAllBusinessCardAsync();
        }

        public async Task<List<BusinessCardRecordDTO>> SearchOnBusinessCard(string input)
        {
            var result = new List<BusinessCardRecordDTO>();
            var getAllBusinessCard = await _unitOfWork.BusinessCards.GetAllBusinessCardAsync();
            if (getAllBusinessCard != null)
            {
                result = getAllBusinessCard.Where(x => x.Name.Contains(input)
                      || x.DateOfBirth.Contains(input)
                      || x.Phone.Contains(input)
                      || x.Gendear.Contains(input)
                      || x.Email.Contains(input)).ToList();
                return result;
            }
            else
            {
                throw new ArgumentException("Business Card  not found");
            }
        }
        public async Task<bool> CreateBusinessCardAsync(CreateBusinessCardDTO businessCardDto)
        {
            BusinessCard businessCard = new BusinessCard()
            {
                Name = businessCardDto.Name,
                Gendear = (Gendear)Enum.Parse(typeof(Gendear), businessCardDto.Gendear, true),
                Email = businessCardDto.Email,
                Phone = businessCardDto.Phone,
                PhotoPath = businessCardDto.Photo,
                Address = businessCardDto.Address,
                DateOfBirth = businessCardDto.DateOfBirth,
            };
            await _unitOfWork.BusinessCards.AddAsync(businessCard);
            await _unitOfWork.SaveAsync();
            return true;

        }

        public async Task<bool> DeleteBusinessCard(int businessCardId)
        {
            var businessCard = await _unitOfWork.BusinessCards.GetByIdAsync(businessCardId);
            if (businessCard == null)
            {
                return false;
            }
            else
            {
                _unitOfWork.BusinessCards.Delete(businessCard);
                await _unitOfWork.SaveAsync();
                return true;

            }
        }
    }
}
