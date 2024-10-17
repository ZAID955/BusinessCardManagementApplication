using BusinessCard_Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCard_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ExportBusinessCardsToXmlFileAsync()
        {
            try
            {
                string filePathXml = await _fileService.ExportBusinessCardsToXmlAsync();
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePathXml);
                return File(fileBytes, "application/xml", Path.GetFileName(filePathXml));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ExportBusinessCardsToCsvFileAsync()
        {
            try
            {
                string filePathCsv = await _fileService.ExportBusinessCardsToCsvAsync();

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePathCsv);
                return File(fileBytes, "text/csv", Path.GetFileName(filePathCsv));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ExportBusinessCardToCsvAsync(int id)
        {
            try
            {
                var csvData = await _fileService.ExportBusinessCardToCsvAsync(id);

                // Return the CSV file for download
                return File(csvData, "text/csv", $"BusinessCard_{id}.csv");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ExportBusinessCardToXmlFileAsync(int id)
        {
            try
            {
                var xmlData = await _fileService.ExportBusinessCardToXmlAsync(id);

                return File(xmlData, "application/xml", $"BusinessCard_{id}.xml");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ImportBusinessCardsFromXmlAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid XML file.");
            }

            var businessCards = await _fileService.ImportBusinessCardsFromXmlAsync(file);
            return Ok(new { Message = $"{businessCards.Count} business cards created successfully." });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ImportBusinessCardsFromCsvAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid CSV file.");
            }

            var businessCards = await _fileService.ImportBusinessCardsFromCsvAsync(file);
            return Ok(new { Message = $"{businessCards.Count} business cards created successfully." });
        }
    }
}
