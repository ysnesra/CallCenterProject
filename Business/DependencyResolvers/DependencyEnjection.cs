using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    //FluentValidation kullanmak için gerekli olan servislerin eklendiği metot
    public static class DependencyEnjection
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

//AddFluentValidationAutoValidation ve AddFluentValidationClientsideAdapters metodları, FluentValidation'ın istemci tarafında çalışması için gerekli olan servisleri IServiceCollection nesnesine ekler.
//AddValidatorsFromAssembly metodu, uygulama içinde bulunan tüm doğrulama kurallarını (validator) IServiceCollection nesnesine ekler. 
