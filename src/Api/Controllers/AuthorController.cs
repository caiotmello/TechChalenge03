using Application.Dtos.Request;
using Application.Services;
using Application.Services.Interface;
using Domain.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAuthorRequestDto auhtorDto)
        {
            try
            {
                var result = await _authorService.CreateAuthorAsync(auhtorDto);
                if (result.IsSuccess)
                    return CreatedAtRoute("GetAuthorById", new { id = result.Data.Id.ToString() }, result);

                return BadRequest(result);
            }
            catch (DomainValidationException ex)
            {
                var result = ResultService.Fail(ex.Message);
                return BadRequest(result);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _authorService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorRequestDto authorDto)
        {
            var result = await _authorService.UpdateAsync(authorDto);
            if (result.IsSuccess)
                return CreatedAtRoute("GetAuthorById", new { id = authorDto.Id.ToString() }, result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
