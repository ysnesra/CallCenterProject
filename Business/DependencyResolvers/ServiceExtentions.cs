using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
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
        public static IServiceCollection ServisRelationShip(this IServiceCollection services)
        {
            services.AddSingleton<ICallService, CallManager>();
            services.AddSingleton<ICallDal, EfCallDal>();

            services.AddSingleton<ICustomerService, CustomerManager>();
            services.AddSingleton<ICustomerDal, EfCustomerDal>();

            services.AddSingleton<ICustomerRepService, CustomerRepManager>();
            services.AddSingleton<ICustomerRepDal, EfCustomerRepDal>();

            services.AddSingleton<IRequestService, RequestManager>();
            services.AddSingleton<IRequestDal, EfRequestDal>();

            services.AddSingleton<ITokenHelper, JwtHelper>();

 
            return services;
        }
    }
}
