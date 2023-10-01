using Application.Dtos.Request;
using Application.Services.Interface;
using Application.Services;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateVideoRequestDto videoDto)
        {
            try
            {
                var result = await _videoService.CreateAsync(videoDto);
                if (result.IsSuccess)
                    return CreatedAtRoute("GetVideoById", new { id = result.Data.Id.ToString() }, result);
                
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = ResultService.Fail(ex.Message);
                return BadRequest(result);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _videoService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}", Name = "GetVideoById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _videoService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateVideoRequestDto videoDto)
        {
            var result = await _videoService.UpdateAsync(videoDto);
            if (result.IsSuccess)
                return CreatedAtRoute("GetVideoById", new { id = videoDto.Id.ToString() }, result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _videoService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
