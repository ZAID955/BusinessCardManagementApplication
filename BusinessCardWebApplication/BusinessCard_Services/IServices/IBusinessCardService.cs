using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Services.IServices
{
    public interface IBusinessCardService
    {
        Task<List<BusinessCardRecordDTO>> GetAllBusinessCardAsync();
        Task<List<BusinessCardRecordDTO>> SearchOnBusinessCard(string input);
        Task<bool> CreateBusinessCardAsync(CreateBusinessCardDTO businessCardDto);
        Task<bool> DeleteBusinessCard(int businessCardId);

    }
}
