using BusinessCard_Core.Models.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Services.IServices
{
    public interface IFileService
    {
        Task<string> ExportBusinessCardsToXmlAsync();
        Task<string> ExportBusinessCardsToCsvAsync();
        Task<byte[]> ExportBusinessCardToCsvAsync(int businessCardId);
        Task<byte[]> ExportBusinessCardToXmlAsync(int businessCardId);
        Task<List<BusinessCard>> ImportBusinessCardsFromXmlAsync(IFormFile file);
        Task<List<BusinessCard>> ImportBusinessCardsFromCsvAsync(IFormFile file);
    }
}
