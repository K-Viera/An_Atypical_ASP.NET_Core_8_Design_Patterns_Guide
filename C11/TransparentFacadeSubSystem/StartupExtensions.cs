using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransparentFacadeSubSystem
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddTransparentFacadeSubSystem(this IServiceCollection services)
        {
            services.AddSingleton<ITransparentFacade, TransparentFacade>();
            services.AddSingleton<IComponentA, ComponentA>();
            services.AddSingleton<IComponentB, ComponentB>();
            services.AddSingleton<IComponentC, ComponentC>();
            return services;
        }
    }
}
