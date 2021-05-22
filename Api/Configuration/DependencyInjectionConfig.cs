using Api.Extension;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Notifications;
using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IMapper, Mapper>();

            services.AddScoped<IStatusRepository, StatusRepository>();            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IJobRoleRepository, JobRoleRepository>();


            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IJobRoleService, JobRoleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<FileService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }
    }
}
