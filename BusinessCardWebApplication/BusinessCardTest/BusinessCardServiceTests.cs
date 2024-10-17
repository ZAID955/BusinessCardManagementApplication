using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Core.Interfaces.UnitOfWorkInterface;
using BusinessCard_Core.Models.Entites;
using BusinessCard_Services.Services;
using Moq;

namespace BusinessCardTest
{
    public class BusinessCardServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly BusinessCardService _businessCardService;

        public BusinessCardServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _businessCardService = new BusinessCardService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAllBusinessCardAsync_ReturnsListOfBusinessCards()
        {
            var businessCards = new List<BusinessCardRecordDTO>
            {
                new BusinessCardRecordDTO { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new BusinessCardRecordDTO { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
            };

            _unitOfWorkMock.Setup(u => u.BusinessCards.GetAllBusinessCardAsync())
                .ReturnsAsync(businessCards);

            var result = await _businessCardService.GetAllBusinessCardAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].Name);
        }

        [Fact]
        public async Task SearchOnBusinessCard_ReturnsMatchingBusinessCards()
        {
            var searchResult = new List<BusinessCardRecordDTO>
            {
                new BusinessCardRecordDTO { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            _unitOfWorkMock.Setup(u => u.BusinessCards.GetAllBusinessCardAsync())
                .ReturnsAsync(searchResult);

            var result = await _businessCardService.SearchOnBusinessCard("John");

            Assert.Single(result);
            Assert.Equal("John Doe", result[0].Name);
        }

        [Fact]
        public async Task CreateBusinessCardAsync_CreatesNewBusinessCard()
        {
            var newBusinessCard = new CreateBusinessCardDTO
            {
                Name = "Alice Brown",
                Gendear = "Female",
                Email = "alice@example.com",
                Phone = "123-456-7890",
                Address = "Some Address",
                DateOfBirth = DateOnly.Parse("1990-01-01")
            };

            _unitOfWorkMock.Setup(u => u.BusinessCards.AddAsync(It.IsAny<BusinessCard>()));
            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            var result = await _businessCardService.CreateBusinessCardAsync(newBusinessCard);

            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.BusinessCards.AddAsync(It.IsAny<BusinessCard>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteBusinessCardAsync_DeletesBusinessCard()
        {
            var businessCard = new BusinessCard { Id = 1, Name = "John Doe" };

            _unitOfWorkMock.Setup(u => u.BusinessCards.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(businessCard);
            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            var result = await _businessCardService.DeleteBusinessCard(1);

            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.BusinessCards.Delete(businessCard), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        }
    }
}

