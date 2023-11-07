﻿using API.Models;

namespace API.Dal.Interfaces
{
    public interface ITopicCommand
    {
        Task<bool> CreateTopic(TopicRequestCreationModel creationModel);
    }
}
