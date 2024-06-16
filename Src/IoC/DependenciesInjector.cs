using Adapter.Database.Postgres.Base;
using Adapter.Out.Postgres.User;
using Adapter.Out.Postgres.User.DAOs;
using Adapter.Out.ThirdPartyLibs;
using Application.Domain.Core.Interfaces;
using Domain.Services.Auth;
using Domain.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Ports.In.Auth;
using Ports.In.User;
using Ports.Out.Auth;
using Ports.Out.User;

namespace Src.IoC;

public static class DependenciesInjector
{
    public static void RegisterServices(IServiceCollection services)
    {
        // ASP.NET HttpContext dependency
        services.AddHttpContextAccessor();

        // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Port services
        services.AddScoped<GetUser, GetUserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPassword, BcryptPassword>();
        services.AddScoped<IAuthenticator, Authenticator>();
        // services.AddScoped<ICache, GarnetDb>();
        services.AddSingleton<PasswordDao>();
    }
}