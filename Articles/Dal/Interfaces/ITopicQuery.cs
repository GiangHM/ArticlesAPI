using Articles.Models;

namespace Articles.Dal.Interfaces
{
    public interface ITopicQuery
    {
        public Task<IEnumerable<TopicResponseModel>> GetAllTopics();
    }
}
