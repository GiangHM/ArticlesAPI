namespace Articles.Models
{
    public class TopicResponseModel
    {
        public long Id { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
