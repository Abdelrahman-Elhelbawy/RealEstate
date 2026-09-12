using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Application.Validators.Property;

namespace RealEstate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePropertyValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePropertyValidator>();
            services.AddScoped<IPropertyService, PropertyService>();

            return services;
        }
    }
}