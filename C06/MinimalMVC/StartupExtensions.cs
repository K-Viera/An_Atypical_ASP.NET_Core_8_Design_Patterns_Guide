using MinimalMVC.Data;

namespace MinimalMVC
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomerRepository(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
