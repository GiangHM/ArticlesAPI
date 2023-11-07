using API.Dal.Commands;
using API.Dal.Interfaces;
using API.Dal.Queries;
using DataAccessLayerShared;

namespace API.Dal.Extensions
{
    public static class DalServiceCollectionExtensions
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services
            , Action<DalReadOnlyOptions> readOptions
            , Action<DalReadWriteOptions> readwriteOptions)
        {
            services.Configure(readOptions);
            services.Configure(readwriteOptions);
            services.AddTransient<ITopicQuery, TopicQuery>();
            services.AddTransient<ITopicCommand, TopicCommand>();
            return services;
        }
    }
}
