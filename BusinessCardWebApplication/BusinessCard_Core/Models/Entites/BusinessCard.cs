using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessCard_Core.Helpers.Enums.ApplicationLookups;

namespace BusinessCard_Core.Models.Entites
{
    public class BusinessCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gendear Gendear { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
