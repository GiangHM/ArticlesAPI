﻿using API.Models;

namespace API.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicResponseModel>> GetAllTopics();
        Task<bool> CreateTopic(TopicRequestCreationModel creationModel);
    }
}
