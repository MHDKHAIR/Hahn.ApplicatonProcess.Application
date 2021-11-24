using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hahn.ApplicatonProcess.July2021.Data;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Data.Repositories;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.WebServices.Services;
using FluentValidation;
using System.Reflection;
using AutoMapper;
using Hahn.ApplicatonProcess.July2021.WebServices.AutoMapper;

namespace Hahn.ApplicatonProcess.July2021.WebServices.Extentions
{
    public static class Initializer
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration, bool inMemory = true)
        {
            if (inMemory)
                return services.AddDbContext<ApplicationDbContext>(options =>
                         options.UseInMemoryDatabase("HahnDB"));

            return services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("HahnDBConnectionString")));

        }
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAssetRepository, AssetRepository>()
                .AddTransient<IHttpRequestRepository, HttpRequestRepository>();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            return services
                .AddScoped<IStoreContext, StoreContext>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAssetsService, AssetsService>()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Scoped)
                .AddSingleton(mapper);
        }
    }
}
