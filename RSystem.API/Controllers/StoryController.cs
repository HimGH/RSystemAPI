using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSystem.Data.Interface;
using RSystem.Model.RequestModel;
using RSystem.Model.ResponseModel;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryRepository _storyRepository;
        private readonly ILogger<StoryController> _logger;

        public StoryController(ILogger<StoryController> logger, IStoryRepository storyRepository)
        {
            _logger = logger;
            _storyRepository = storyRepository;
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
                _logger.LogTrace("Success");
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
