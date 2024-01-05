using Azure;
using Azure.Data.Tables;

namespace API.Entities
{
    public class TopicEntity : ITableEntity
    {
        public TopicEntity()
        {
        }

        public string PartitionKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RowKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset? Timestamp { get ; set ; } = default;
        public ETag ETag { get; set; } = default;
    }
}
