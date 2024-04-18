namespace FeedbackApp.Crosscutting.Configuration
{
    using FeedbackApp.Application.DTOs;
    using FeedbackApp.Application.Interfaces;
    using FeedbackApp.Application.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstrapper
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFeedbackService, FeedbackService>();

            return services;
        }
    }
}
