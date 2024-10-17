using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Core.Models
{
    public class BusinessCardCsvModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gendear { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
    }
}
