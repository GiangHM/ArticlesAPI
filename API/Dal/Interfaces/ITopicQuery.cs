using API.Models;

namespace API.Dal.Interfaces
{
    public interface ITopicQuery
    {
        public Task<IEnumerable<TopicResponseModel>> GetAllTopics();
    }
}
