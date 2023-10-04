using Application.Dtos.Request;
using Application.Services;
using Application.Services.Interface;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateArticleRequestDto articleDto)
        {
            try
            {
                var result = await _articleService.CreateAsync(articleDto);
                if (result.IsSuccess)
                    return CreatedAtRoute("GetArticleById", new { id = result.Data.Id.ToString() }, result);

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
            var result = await _articleService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}", Name= "GetArticleById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _articleService.GetByIdAsync(id);
            if(result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateArticleRequestDto articleDto)
        {
            var result = await _articleService.UpdateAsync(articleDto);
            if(result.IsSuccess)
                return CreatedAtRoute("GetArticleById", new { id = articleDto.Id.ToString() }, result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _articleService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
