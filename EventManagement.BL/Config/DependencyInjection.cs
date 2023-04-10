namespace EventManagement.BL.Config
{
    using EventManagement.BL.Services.Event.DeleteById;
    using EventManagement.BL.Services.Event.Get;
    using EventManagement.BL.Services.Event.Post;
    using EventManagement.BL.Services.Event.Put;
    using EventManagement.BL.Services.User.DeleteById;
    using EventManagement.BL.Services.User.Get;
    using EventManagement.BL.Services.User.Post;
    using EventManagement.BL.Services.User.Put;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddBlAssembly(this IServiceCollection services) { 
            // Event services
            services.AddScoped<IEventGetService, EventGetService>();
            services.AddScoped<IEventPostService, EventPostService>();
            services.AddScoped<IEventGetByIdService, EventGetByIdService>();
            services.AddScoped<IEventPutService, EventPutService>();
            services.AddScoped<IEventDeleteByIdService, EventDeleteByIdService>();

            // User Services
            services.AddScoped<IUserGetService, UserGetService>();
            services.AddScoped<IUserPostService, UserPostService>();
            services.AddScoped<IUserGetByIdService, UserGetByIdService>();
            services.AddScoped<IUserPutService, UserPutService>();
            services.AddScoped<IUserDeleteByIdService, UserDeleteByIdService>();

            return services;
        }

    }
}
