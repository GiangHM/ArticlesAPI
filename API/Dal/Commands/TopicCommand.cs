using API.Dal.Interfaces;
using API.Models;
using DataAccessLayerShared;

namespace API.Dal.Commands
{
    public class TopicCommand : DalBaseReadWrite, ITopicCommand
    {
        public TopicCommand(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<bool> CreateTopic(TopicRequestCreationModel creationModel)
        {
            var parameters = new Dictionary<string, object>
            {
                { "TopicName", creationModel.TopicName },
                { "TopicDescription ", creationModel.TopicDescription }
            };
            using (var connection = await GetConnection())
            {
                await ExecuteWithoutReturn(connection, "AddNewTopic", parameters);
            }
            return true;
        }
    }
}
