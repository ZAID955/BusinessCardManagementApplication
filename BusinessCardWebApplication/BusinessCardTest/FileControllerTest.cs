using BusinessCard_API.Controllers;
using BusinessCard_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessCardTests
{
    public class FilesControllerTests
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly FilesController _filesController;

        public FilesControllerTests()
        {
            _fileServiceMock = new Mock<IFileService>();
            _filesController = new FilesController(_fileServiceMock.Object);
        }

        [Fact]
        public async Task ExportBusinessCardsToXmlFileAsync_ReturnsFileResult()
        {
            var fakeFilePath = "fakepath.xml";
            _fileServiceMock.Setup(s => s.ExportBusinessCardsToXmlAsync())
                .ReturnsAsync(fakeFilePath);
            var fileBytes = Encoding.UTF8.GetBytes("<xml></xml>");
            File.WriteAllBytes(fakeFilePath, fileBytes);

            var result = await _filesController.ExportBusinessCardsToXmlFileAsync();

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/xml", fileResult.ContentType);
        }

        [Fact]
        public async Task ExportBusinessCardsToCsvFileAsync_ReturnsFileResult()
        {
            var fakeFilePath = "fakepath.csv";
            _fileServiceMock.Setup(s => s.ExportBusinessCardsToCsvAsync())
                .ReturnsAsync(fakeFilePath);
            var fileBytes = Encoding.UTF8.GetBytes("Name,Phone,Email\nJohn Doe,123456789,johndoe@example.com");
            File.WriteAllBytes(fakeFilePath, fileBytes);

            var result = await _filesController.ExportBusinessCardsToCsvFileAsync();

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("text/csv", fileResult.ContentType);
        }

        [Fact]
        public async Task ImportBusinessCardsFromXmlAsync_ReturnsBadRequestForInvalidFile()
        {
            var result = await _filesController.ImportBusinessCardsFromXmlAsync(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Please upload a valid XML file.", badRequestResult.Value);
        }

        [Fact]
        public async Task ImportBusinessCardsFromCsvAsync_ReturnsBadRequestForInvalidFile()
        {
            var result = await _filesController.ImportBusinessCardsFromCsvAsync(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Please upload a valid CSV file.", badRequestResult.Value);
        }

        [Fact]
        public async Task ExportBusinessCardToCsvAsync_ValidId_ReturnsFileResult()
        {
            int validId = 1;
            var csvData = Encoding.UTF8.GetBytes("Name,Phone,Email\nJohn Doe,123456789,johndoe@example.com");
            _fileServiceMock.Setup(s => s.ExportBusinessCardToCsvAsync(validId))
                .ReturnsAsync(csvData);

            var result = await _filesController.ExportBusinessCardToCsvAsync(validId);

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("text/csv", fileResult.ContentType);
            Assert.Equal($"BusinessCard_{validId}.csv", fileResult.FileDownloadName);
        }

        [Fact]
        public async Task ExportBusinessCardToCsvAsync_InvalidId_ReturnsNotFound()
        {
            int invalidId = 999;
            _fileServiceMock.Setup(s => s.ExportBusinessCardToCsvAsync(invalidId))
                .ThrowsAsync(new ArgumentException("Business card not found"));

            var result = await _filesController.ExportBusinessCardToCsvAsync(invalidId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Business card not found", notFoundResult.Value);
        }

        [Fact]
        public async Task ExportBusinessCardToXmlFileAsync_ValidId_ReturnsFileResult()
        {
            int validId = 1;
            var xmlData = Encoding.UTF8.GetBytes("<BusinessCard><Name>John Doe</Name></BusinessCard>");
            _fileServiceMock.Setup(s => s.ExportBusinessCardToXmlAsync(validId))
                .ReturnsAsync(xmlData);

            var result = await _filesController.ExportBusinessCardToXmlFileAsync(validId);

            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/xml", fileResult.ContentType);
            Assert.Equal($"BusinessCard_{validId}.xml", fileResult.FileDownloadName);
        }

        [Fact]
        public async Task ExportBusinessCardToXmlFileAsync_InvalidId_ReturnsNotFound()
        {
            int invalidId = 999;
            _fileServiceMock.Setup(s => s.ExportBusinessCardToXmlAsync(invalidId))
                .ThrowsAsync(new ArgumentException("Business card not found"));

            var result = await _filesController.ExportBusinessCardToXmlFileAsync(invalidId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Business card not found", notFoundResult.Value);
        }

        [Fact]
        public async Task ExportBusinessCardToCsvAsync_UnexpectedError_ReturnsServerError()
        {
            int id = 1;
            _fileServiceMock.Setup(s => s.ExportBusinessCardToCsvAsync(id))
                .ThrowsAsync(new Exception("Unexpected error"));

            var result = await _filesController.ExportBusinessCardToCsvAsync(id);

            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, serverErrorResult.StatusCode);
            Assert.Equal("Unexpected error", serverErrorResult.Value);
        }

        [Fact]
        public async Task ExportBusinessCardToXmlFileAsync_UnexpectedError_ReturnsServerError()
        {
            int id = 1;
            _fileServiceMock.Setup(s => s.ExportBusinessCardToXmlAsync(id))
                .ThrowsAsync(new Exception("Unexpected error"));

            var result = await _filesController.ExportBusinessCardToXmlFileAsync(id);

            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, serverErrorResult.StatusCode);
            Assert.Equal("Unexpected error", serverErrorResult.Value);
        }

    }
}

