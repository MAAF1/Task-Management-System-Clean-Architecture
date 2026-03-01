using System.Reflection;
using Application.Contracts.Services;
using Application.Features.Tasks;
using Application.Features.Users;
using Application.Options.Jwt;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServices).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationServices).Assembly));
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskUserService, TaskUserService>();
            services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
