using Microsoft.Extensions.DependencyInjection;
using MyDotNetProject.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyDotNetProject.Common
{
    public static class InjectionInit
    {
        /// <summary>
        ///     调用容器注入初始化.
        /// </summary>
        /// <param name="services">容器.</param>
        /// <param name="assemblyName">程序集.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection Injection(this IServiceCollection services, string assemblyName)
        {
            // 注入所有的依赖.
            services.InjectionByAssemblyName(assemblyName);

            var types = GetClass(assemblyName);

            foreach (var type in types)
            {
                services.InjectionAdd(type, null);
            }

            return services;
        }

        private static void InjectionByAssemblyName(this IServiceCollection services, string assemblyName)
        {
            //注入所有的依赖
            Dictionary<Type, Type[]> all = GetClassesInterface(assemblyName);

            foreach (var item in all)
            {
                foreach (var type in item.Value)
                {
                    services.InjectionAdd(type, item.Key);
                }
            }
        }

        private static Dictionary<Type, Type[]> GetClassesInterface(string assemblyName)
        {
            var injectionType = typeof(IInjection);

            Dictionary<Type, Type[]> result = new Dictionary<Type, Type[]>();
            if (String.IsNullOrEmpty(assemblyName))
            {
                return result;
            }

            Assembly assembly = Assembly.Load(assemblyName);
            List<Type> types = assembly.GetTypes().Where(t => t.IsClass && !t.IsInterface && !t.IsGenericType && !t.IsAbstract && injectionType.IsAssignableFrom(t)).ToList();

            foreach (var item in types)
            {
                Type[] interfaceType = item.GetInterfaces();
                if (interfaceType != null && interfaceType.Length > 0)
                {
                    result.Add(item, interfaceType);
                }
            }
            return result;
        }

        public static List<Type> GetClass(string assemblyName)
        {
            var injectionType = typeof(IInjectionClass);

            List<Type> result = new List<Type>();

            if (String.IsNullOrEmpty(assemblyName))
            {
                return result;
            }

            Assembly assembly = Assembly.Load(assemblyName);
            List<Type> types = assembly.GetTypes().Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract && !t.IsGenericType && injectionType.IsAssignableFrom(t)).ToList();

            return types;
        }

        private static void InjectionAdd(this IServiceCollection services, Type type, Type implementationType)
        {
            var life = type.GetCustomAttribute<InjectionLifeAttribute>();

            if (life == null)
            {
                if (implementationType == null)
                {
                    services.AddScoped(type);
                }
                else
                {
                    services.AddScoped(type, implementationType);
                }

                return;
            }

            switch (life.Life)
            {
                case IInjectionLifeType.Singleton:
                    {
                        if (implementationType == null)
                        {
                            services.AddSingleton(type);
                        }
                        else
                        {
                            services.AddSingleton(type, implementationType);
                        }
                    }
                    break;
                case IInjectionLifeType.Transient:
                    {
                        if (implementationType == null)
                        {
                            services.AddTransient(type);
                        }
                        else
                        {
                            services.AddTransient(type, implementationType);
                        }
                    }
                    break;
                case IInjectionLifeType.Scoped:
                default:
                    {
                        if (implementationType == null)
                        {
                            services.AddScoped(type);
                        }
                        else
                        {
                            services.AddScoped(type, implementationType);
                        }
                    }
                    break;
            }
        }
    }
}
