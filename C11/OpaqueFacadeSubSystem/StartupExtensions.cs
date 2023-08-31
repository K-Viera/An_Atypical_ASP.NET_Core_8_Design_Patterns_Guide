using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpaqueFacadeSubSystem
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddOpaqueFacadeSubSystem(this IServiceCollection services)
        {
            services.AddSingleton<IOpaqueFacade>(ServiceProvider
                => new OpaqueFacade(new ComponentA(), new ComponentB(), new ComponentC()));
            return services;
        }
    }
}
