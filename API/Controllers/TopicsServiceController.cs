using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicServiceController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicServiceController> _logger;

        public TopicServiceController(ITopicService topicService
            , ILogger<TopicServiceController> logger)
        {
            _topicService = topicService;
            _logger = logger;
        }

        [HttpGet("Topics")]
        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
            _logger.LogInformation("If you're seeing this, we get all topics");
            return await _topicService.GetAllTopics();
        }

        [HttpPost("Topics")]
        public async Task<bool> CreateTopic([FromBody] TopicRequestCreationModel creationModel)
        {
            return await _topicService.CreateTopic(creationModel);
        }

        [HttpPut("Topics/{id}")]
        public async Task<bool> UpdateTopic([FromRoute] long id, [FromQuery] bool isDeactivated)
        {
            return await _topicService.UpdateTopic(id, isDeactivated);
        }
    }
}
