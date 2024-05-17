﻿using ApiTemplate.Repositories;

namespace ApiTemplate.Config
{
    public static class ServicesInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<IVotoRepository, VotoRepository>();
            return services;
        }   
    }
}
