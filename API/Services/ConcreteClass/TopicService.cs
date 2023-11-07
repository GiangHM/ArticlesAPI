using API.Dal.Interfaces;
using API.Models;
using API.Services.Interfaces;

namespace API.Services.ConcreteClass
{
    public class TopicService : ITopicService
    {
        private readonly ITopicQuery _topicQuery;
        private readonly ITopicCommand _topicCommand;
        public TopicService(ITopicQuery topicQuery
            , ITopicCommand topicCommand)
        {
            _topicQuery = topicQuery;
            _topicCommand = topicCommand;
        }

        public async Task<bool> CreateTopic(TopicRequestCreationModel creationModel)
        {
            return await _topicCommand.CreateTopic(creationModel);
        }

        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
            return await _topicQuery.GetAllTopics();
        }
        public async Task<bool> UpdateTopic(long id, bool deactivated)
        {
            return await _topicCommand.UpdateTopic(id, deactivated);
        }
    }
}
