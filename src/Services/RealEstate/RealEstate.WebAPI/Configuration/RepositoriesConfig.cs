using RealEstate.Application.Interfaces.IData;
using RealEstate.Application.Interfaces.IRepository.Blog;
using RealEstate.Application.Interfaces.IRepository.RealEstate;
using RealEstate.Application.Interfaces.IRepository.Setting;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Repositories.Blog;
using RealEstate.Infrastructure.Repositories.RealEstate;
using RealEstate.Infrastructure.Repositories.Setting;

namespace RealEstate.WebAPI.Configuration
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IConstantRepository, ConstantRepository>();
            services.AddScoped<IPropertyInventoryRepository, PropertyInventoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            return services;
        }
    }
}
