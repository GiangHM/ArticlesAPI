using Articles.Dal.Interfaces;
using Articles.Models;
using Articles.Services.Interfaces;

namespace Articles.Services.ConcreteClass
{
    public class TopicService : ITopicService
    {
        private readonly ITopicQuery _topicQuery;
        public TopicService(ITopicQuery topicQuery)
        {
            _topicQuery = topicQuery;
        }
        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
            return await _topicQuery.GetAllTopics();
        }
    }
}
