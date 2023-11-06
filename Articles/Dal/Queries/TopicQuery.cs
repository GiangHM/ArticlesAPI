using Articles.Dal.Interfaces;
using Articles.Models;
using DataAccessLayerShared;

namespace Articles.Dal.Queries
{
    public class TopicQuery : DALBaseReadOnly, ITopicQuery
    {
        public TopicQuery(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<IEnumerable<TopicResponseModel>> GetAllTopics()
        {
            var result = new List<TopicResponseModel>();
            using (var connection = await GetConnection())
            {
                using (var dr = await ExecuteForData(connection, "GetAllTopic"))
                {
                    while (dr.Read())
                    {
                        var model = new TopicResponseModel();
                        model.Id = dr.GetDBValue<long>("Id");
                        model.TopicName = dr.GetDBValue<string>("TopicName");
                        model.TopicDescription = dr.GetDBValue<string>("TopicDescription");
                        model.IsActive = dr.GetDBValue<bool>("IsActive");
                        result.Add(model);
                    }
                }
            }
            return result;
        }
    }
}
