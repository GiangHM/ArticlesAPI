using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayerShared
{
    public abstract class DalBaseReadOnly<T> : DalBase
        where T : DalReadOnlyOptions, new()
    {
        protected DalBaseReadOnly(IServiceProvider serviceProvider)
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


    public abstract class DALBaseReadOnly : DalBaseReadOnly<DalReadOnlyOptions>
    {
        protected DALBaseReadOnly(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
    }
}
