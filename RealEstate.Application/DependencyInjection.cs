using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Application.Validators.Agent;
using RealEstate.Application.Validators.Property;

namespace RealEstate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePropertyValidator>();


            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPropertyImageService, PropertyImageService>();
            services.AddScoped<IPropertyReportService, PropertyReportService>();
            return services;
        }
    }
}