using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Helpers.FileHelper;
using BusinessCard_Core.Models.Entites;
using BusinessCard_Services.IServices;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessCard_Core.Helpers.Enums.ApplicationLookups;
using System.Xml;
using BusinessCard_Core.Interfaces.UnitOfWorkInterface;
using System.Xml.Serialization;
using MySqlX.XDevAPI.Common;
using Mysqlx.Session;

namespace BusinessCard_Services.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessCardService _businessCardService;
        public FileService(IUnitOfWork unitOfWork, IBusinessCardService businessCardService)
        {
            _unitOfWork = unitOfWork;
            _businessCardService = businessCardService;
        }
        public async Task<string> ExportBusinessCardsToXmlAsync()
        {
            var businessCards = await _businessCardService.GetAllBusinessCardAsync();

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string directoryPath = @"C:\businessCardTemp\";
            string fileName = $"businessCards_{timestamp}.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            XmlHelper.ExportToXml(filePath, businessCards.ToList());
            return filePath;
        }

        public async Task<string> ExportBusinessCardsToCsvAsync()
        {
            var businessCards = await _unitOfWork.BusinessCards.GetAllBusinessCardAsync();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,Gendear,Email,Phone,DateOfBirth,Address,Photo");

            foreach (var card in businessCards)
            {
                csvBuilder.AppendLine($"{card.Id},{card.Name},{card.Gendear.ToString()},{card.Email},{card.Phone},{card.DateOfBirth:yyyy-MM-dd},{card.Address},{card.Photo}");
            }

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string directoryPath = @"C:\businessCardTemp\";
            string fileName = $"businessCards_{timestamp}.csv";
            string filePath = Path.Combine(directoryPath, fileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            byte[] csvData = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            await System.IO.File.WriteAllBytesAsync(filePath, csvData);
            return filePath;
        }

        public async Task<byte[]> ExportBusinessCardToCsvAsync(int businessCardId)
        {
            var businessCard = await _unitOfWork.BusinessCards.GetByIdAsync(businessCardId);

            if (businessCard == null)
            {
                throw new ArgumentException("BusinessCard not found.");
            }

            var csvData = new StringBuilder();
            csvData.AppendLine("Id,Name,Gendear,Email,Phone,DateOfBirth,Address,Photo");

            csvData.AppendLine($"{businessCard.Id},{businessCard.Name},{businessCard.Gendear},{businessCard.Email},{businessCard.Phone},{businessCard.DateOfBirth:yyyy-MM-dd},{businessCard.Address},{businessCard.PhotoPath}");
            return Encoding.UTF8.GetBytes(csvData.ToString());
        }

        public async Task<byte[]> ExportBusinessCardToXmlAsync(int businessCardId)
        {
            var businessCard = await _unitOfWork.BusinessCards.GetByIdAsync(businessCardId);

            if (businessCard == null)
            {
                throw new ArgumentException("BusinessCard not found.");
            }

            var xmlSerializer = new XmlSerializer(typeof(BusinessCard));
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);

            xmlSerializer.Serialize(xmlWriter, businessCard);

            return Encoding.UTF8.GetBytes(stringWriter.ToString());
        }

        public async Task<List<BusinessCard>> ImportBusinessCardsFromXmlAsync(IFormFile file)
        {
            var businessCards = new List<BusinessCard>();
            using (var stream = file.OpenReadStream())
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                foreach (XmlNode node in xmlDoc.SelectNodes("//BusinessCardRecordDTO"))
                {

                    var businessCard = new BusinessCard
                    {
                        Name = node["Name"].InnerText,
                        Phone = node["Phone"].InnerText,
                        Email = node["Email"].InnerText,
                        PhotoPath = node["Photo"].InnerText,
                        Address = node["Address"].InnerText,
                        Gendear = (Gendear)Enum.Parse(typeof(Gendear), node["Gendear"].InnerText, true),
                        DateOfBirth = DateOnly.Parse(node["DateOfBirth"].InnerText)
                    };
                    businessCards.Add(businessCard);
                }
            }

            foreach (var card in businessCards)
            {
                await _unitOfWork.BusinessCards.AddAsync(card);
            }

            await _unitOfWork.SaveAsync();
            return businessCards;
        }

        public async Task<List<BusinessCard>> ImportBusinessCardsFromCsvAsync(IFormFile file)
        {
            var businessCards = new List<BusinessCard>();

            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                var records = csv.GetRecords<BusinessCardRecordDTO>().ToList();
                foreach (var record in records)
                {
                    var businessCard = new BusinessCard
                    {
                        Name = record.Name,
                        PhotoPath = record.Photo,
                        Email = record.Email,
                        Phone = record.Phone,
                        DateOfBirth = DateOnly.Parse(record.DateOfBirth),
                        Address = record.Address,
                        Gendear = (Gendear)Enum.Parse(typeof(Gendear), record.Gendear, true),
                    };

                    businessCards.Add(businessCard);
                    await _unitOfWork.BusinessCards.AddAsync(businessCard);
                }
            }

            await _unitOfWork.SaveAsync();
            return businessCards;
        }

    }
}
