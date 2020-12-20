using System;
using Microsoft.Extensions.DependencyInjection;

namespace Lingos.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPlugin<T>(this IServiceCollection collection, string relativePath)
        {
            T obj = Factory.CreatePlugin<T>(Factory.LoadPlugin(relativePath));
            
            return collection.AddTransient(typeof(T));
        }

        public static IServiceCollection AddSources(this IServiceCollection)
        { 
            string sourcePath = "Lingos.Source.Postgres/bin/Debug/net5.0/Lingos.Source.Postgres.dll";
            Factory.GetSource(sourcePath);
        }
    }
}