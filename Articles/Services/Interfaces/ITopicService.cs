using Articles.Models;

namespace Articles.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicResponseModel>> GetAllTopics();
    }
}
