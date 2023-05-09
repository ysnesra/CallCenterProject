using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Business.DependencyResolvers
{
    public static class ServiceExtentions 
    {
        public static IServiceCollection ServisRelationShip(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ICallService, CallManager>();
            services.AddScoped<ICallDal, EfCallDal>();

            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<ICustomerDal, EfCustomerDal>();

            services.AddScoped<ICustomerRepService, CustomerRepManager>();
            services.AddScoped<ICustomerRepDal, EfCustomerRepDal>();

            services.AddScoped<IRequestService, RequestManager>();
            services.AddScoped<IRequestDal, EfRequestDal>();

            services.AddScoped<IAdminService, AdminManager>();
           

            services.AddDbContext<CallCenterDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });

            return services;
        }
    }
}
