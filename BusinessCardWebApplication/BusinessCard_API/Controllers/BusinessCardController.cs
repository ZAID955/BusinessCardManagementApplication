using BusinessCard_Core.Dtos.BusinessCardDtos;
using BusinessCard_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCard_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController : ControllerBase
    {
        private readonly IBusinessCardService _businessCardService;
        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllBusinessCardAsync()
        {
            try
            {
                var result = await _businessCardService.GetAllBusinessCardAsync();
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> FilterOnBusinessCard(string input)
        {
            try
            {
                var result = await _businessCardService.SearchOnBusinessCard(input);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateBusinessCardAsync(CreateBusinessCardDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Please Fill All Data");
            }
            else
            {
                try
                {
                    bool result = await _businessCardService.CreateBusinessCardAsync(dto);
                    if (result == true)
                        return Ok("Business Card Created successfully");
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(503);
                }

                return BadRequest("Could not Create the Business Card.");
            }
        }

        [HttpDelete]
        [Route("[action]/{Id}")]
        public async Task<IActionResult> DeleteBusinessCard([FromRoute] int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Please Fill All Data");
            }
            else
            {
                try
                {
                    await _businessCardService.DeleteBusinessCard(Id);
                    return StatusCode(200, "Business Card Has Been Deleted");
                }
                catch (Exception ex)
                {
                    return StatusCode(503);
                }
            }
        }


    }




}
