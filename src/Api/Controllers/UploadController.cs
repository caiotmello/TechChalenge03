using Application.Services;
using Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(IFormFile file)
        {
            try
            {
                var result = await _uploadService.UploadFileAsync(file);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = ResultService.Fail(ex.Message);
                return StatusCode(500, result);
            }
        }
    }
}
