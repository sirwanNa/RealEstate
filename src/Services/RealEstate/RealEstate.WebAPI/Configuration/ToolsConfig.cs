using System.Reflection;
using RealEstate.WebAPI.Filters;

namespace RealEstate.WebAPI.Configuration
{
    public static class ToolsConfig
    {
        public static IServiceCollection ConfigureMediatR(this IServiceCollection services,string assembly) 
        {
            var assemblies = new[] { Assembly.GetExecutingAssembly(), Assembly.Load(assembly) };
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
            return services;
        }
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            return services;
        }
    }
}
