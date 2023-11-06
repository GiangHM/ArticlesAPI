using Articles.Models;
using Articles.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;

        }

        [HttpGet("topics")]
        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
            return await _topicService.GetAllTopics();
        }
    }
}
