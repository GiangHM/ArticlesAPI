using Articles.Dal.Interfaces;
using Articles.Dal.Queries;
using DataAccessLayerShared;

namespace Articles.Dal.Extensions
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
            return services;
        }
    }
}
