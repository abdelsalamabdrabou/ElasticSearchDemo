using ElasticSearchDemo.Models;
using ElasticSearchDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService;

        public LogController(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpPost("TestLogging")]
        public async Task<IActionResult> TestLogging()
        {
            await _elasticsearchService.TestLogging();

            return Ok();
        }

        [HttpPost("CreateIndex")]
        public async Task<IActionResult> CreateIndex([FromBody] CreateIndexDto createIndexDto)
        {
            var response = await _elasticsearchService.CreateIndex(createIndexDto);

            if (response.Contains("failed"))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddDocument")]
        public async Task<IActionResult> AddDocument([FromBody] CreateDocument<LogModel> createDocument)
        {
            var response = await _elasticsearchService.AddDocument(createDocument);

            if (response.Contains("failed"))
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("SearchDocument")]
        public async Task<IActionResult> SearchDocument([FromBody] SearchKeyword searchKeyword)
        {
            var response = await _elasticsearchService.SearchDocument<LogModel>(searchKeyword);

            if (response == default)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("DeleteDocument")]
        public async Task<IActionResult> DeleteDocument([FromBody] DeleteDocumentDto deleteDocumentDto)
        {
            var response = await _elasticsearchService.DeleteDocument(deleteDocumentDto);

            if (response.Contains("failed"))
                return BadRequest(response);

            return Ok(response);
        }
    }
}
