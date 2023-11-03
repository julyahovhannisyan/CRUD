using CRUD.BusinessLayer.Services;
using CRUD.BusinessLayer.Services.Implementation;
using CRUD.DataAccessLayer.Cache.Options;
using CRUD.DataAccessLayer.Cache.Services;
using CRUD.DataAccessLayer.SQL.Repository;
using CRUD.DataAccessLayer.SQL.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CRUD.Composition;

public static class CompositionRoot
{
    public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
    {
        RegisterContext(services);
        RegisterServices(services);
        RegisterOptions(services, configuration);
        RegisterCache(services, configuration);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICacheService, CacheService>();
    }

    private static void RegisterContext(IServiceCollection services)
    {
        services.AddDbContext<CRUDContext>(
                options => options.UseMySql("server=localhost; port=3306; database=ArticlesDB; user=root; password=123456; Persist Security Info=False; Connect Timeout=300",
                ServerVersion.AutoDetect("server=localhost; port=3306; database=ArticlesDB; user=root; password=123456; Persist Security Info=False; Connect Timeout=300")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void RegisterOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisConfiguration>(configuration.GetSection(nameof(RedisConfiguration)));
    }

    private static void RegisterCache(IServiceCollection services, IConfiguration configuration)
    {

        var redisOptions = configuration.GetSection(nameof(RedisConfiguration)).Get<RedisConfiguration>();

        ConfigurationOptions options = ConfigurationOptions.Parse(redisOptions.ConnectionString);

        options.AbortOnConnectFail = false;

        services.AddSingleton(s => ConnectionMultiplexer.Connect(options));

        services.AddSingleton(s => s.GetRequiredService<ConnectionMultiplexer>().GetDatabase());

        services.AddSingleton<ICacheService, CacheService>();

    }
}