using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayerShared
{
    public abstract class DalBaseReadWrite<T> : DalBase
        where T : DalReadWriteOptions, new()
    {
        protected DalBaseReadWrite(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
        protected override string GetConnectionString(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IOptions<T>>().Value.ConnexionString;
        }
        protected override string GetSqlProviderName(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IOptions<T>>().Value.ProviderName;
        }

    }

    public class DALBaseReadWrite : DalBaseReadWrite<DalReadWriteOptions>
    {
        protected DALBaseReadWrite(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
    }
}
