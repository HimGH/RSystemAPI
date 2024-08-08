using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSystem.Data.Interface;
using RSystem.Model.RequestModel;
using RSystem.Model.ResponseModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IStoryRepository _storyRepository;
        private readonly ILogger<StoryController> _logger;

        public StoryController(ILogger<StoryController> logger, HttpClient httpClient, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get Stories 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet("GetStories")]
        [ProducesResponseType(type: typeof(ClientResponseListModel<object>), statusCode: 200)]
        public async Task<IActionResult> GetStories([FromQuery] PaginationParameters pagination)
        {

         var response =  await _storyRepository.GetStories(pagination);
            if (response != null)
            {
                return Ok(new ClientResponseListModel<object>()
                {
                    data = response,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    TotalCount = response.FirstOrDefault().total
                });
            }
            else
            {
                return Ok(new ClientResponseListModel<object>()
                {
                    data = null,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    TotalCount = 0
                });
            }

        }
    }
}
