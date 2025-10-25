using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuotesManagement.Application.Interfaces;
using QuotesManagement.Application.Service;
using QuotesManagement.Common.DTO_s;
using QuotesManagement.Common.DTO_s.reqSearchDTO;
using QuotesManagement.Common.DTO_s.ResponseDTO;
using QuotesManagement.Common.DTO_s.SearchDTO;

namespace QuotesManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAngular")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _service;

        public QuotesController(IQuotesService service)
        {
            _service = service;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<List<resqponseQuotesDto>>> GetAll()
        {
            var quotes = await _service.GetAllAsync();
            return Ok(quotes);
        }

        [HttpGet("GetBy {id}")]
        public async Task<ActionResult<resqponseQuotesDto>> GetById(int id)
        {
            var quote = await _service.GetByIdAsync(id);
            if (quote == null) return NotFound();
            return Ok(quote);
        }

        [HttpPost("post")]
        public async Task<ActionResult<resqponseQuotesDto>> Create([FromBody]searchrequestDto dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("PostBy/{id}")]
        public async Task<ActionResult<resqponseQuotesDto>> Update(int id, [FromBody]searchrequestDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("DeleteBy/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] searchDto dto)
        {
            var results = await _service.SearchAsync(dto.authorName, dto.tags, dto.quotes);
            return Ok(results);
        }
        [HttpPost("add-multiple")]
        public async Task<IActionResult> AddMultipleQuotes([FromBody] RequestSearchDto dto)
        {
            if (dto.Quote == null || !dto.Quote.Any())
                return BadRequest("No quotes provided.");

            var result = await _service.AddMultipleQuotesAsync(dto.Quote); // dto.Quote is List<QuoteDto>

            return Ok(result); // return list of added quotes
        }

    }
}
