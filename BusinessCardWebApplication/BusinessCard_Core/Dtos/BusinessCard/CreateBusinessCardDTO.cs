using static BusinessCard_Core.Helpers.Enums.ApplicationLookups;

namespace BusinessCard_Core.Dtos.BusinessCardDtos
{
    public class CreateBusinessCardDTO
    {
        public string Name { get; set; }
        public string Gendear { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
    }
}
