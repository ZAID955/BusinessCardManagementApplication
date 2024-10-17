using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessCard_Core.Helpers.FileHelper
{
    public static class XmlHelper
    {
        public static void ExportToXml(string filePath, List<BusinessCardRecordDTO> businessCards)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<BusinessCardRecordDTO>));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, businessCards);
            }
        }
    }
}

