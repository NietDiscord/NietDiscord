﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NietDiscord.SocketManager
{
    public static class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                    services.AddSingleton(type);
            }
            return services;
        }

        public static IApplicationBuilder mapSockets(this IApplicationBuilder app, PathString path,
            SocketHandler socket)
        {
            return app.Map(path, (x) => x.UseMiddleware<SocketMiddelware>(socket));
        }
    }
}