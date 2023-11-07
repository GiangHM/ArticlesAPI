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

        public TopicServiceController(ITopicService topicService)
        {
            _topicService = topicService;

        }

        [HttpGet("Topics")]
        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
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
