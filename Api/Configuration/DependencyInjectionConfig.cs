using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Notifications;
using Domain.Services;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IJobRoleRepository, JobRoleRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
