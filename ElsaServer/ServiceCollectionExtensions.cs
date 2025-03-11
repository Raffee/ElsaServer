using Elsa.Workflows;
using Elsa;
using Genesis.Common.Core;
using Genesis.Common.Modules;
using Genesis.Common.ServiceProviders;
using System.Reflection;
using System.Runtime.Loader;
using Elsa.Features.Services;
using Elsa.Extensions;

namespace Genesis.Core.WorkflowServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadBusinessModules(this IServiceCollection services)
        {
            // Load assemblies dynamically
            string pluginsPath = @"C:\Users\Raffee.Parseghian\OneDrive - Octopus Group\Documents\__repos\_test\ElsaServer\ElsaServer\ModulePlugins";
            var loadedAssemblies = LoadAssembliesFromFolder(pluginsPath);

            // Register services that implement IGenesisModule from loaded assemblies
            foreach (var assembly in loadedAssemblies)
            {
                var types = assembly.GetTypes().Where(t => typeof(IGenesisModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                foreach (var type in types)
                {
                    services.AddSingleton(typeof(IGenesisModule), type);
                }

                //get workflow classes
                var workflowTypes = assembly.GetTypes().Where(t => typeof(WorkflowBase).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            }

            return services;
        }

        public static IServiceCollection LoadServiceProviders(this IServiceCollection services)
        {
            //Get the service registry
            var serviceRegistry = services.BuildServiceProvider().GetService<ServiceRegistry>();
            // Load assemblies dynamically
            string pluginsPath = @"C:\Users\Raffee.Parseghian\OneDrive - Octopus Group\Documents\__repos\_test\ElsaServer\ElsaServer\AdapterPlugins";
            var loadedAssemblies = LoadAssembliesFromFolder(pluginsPath);

            // Register services that implement IGenesisModule from loaded assemblies
            foreach (var assembly in loadedAssemblies)
            {
                var types = assembly.GetTypes();//.Where(t => typeof(IGenesisServiceProvider).);// && !t.IsInterface && !t.IsAbstract);
                foreach (var type in types)
                {
                    //get the Class of type so that I can use it in dependency injection
                    var classType = type.GetTypeInfo();


                    var implementedTypes = ((System.Reflection.TypeInfo)type).ImplementedInterfaces;

                    if (!implementedTypes.Contains(typeof(IGenesisServiceProvider)))
                    {
                        continue;
                    }
                    foreach (var implementedType in implementedTypes)
                    {
                        if (type.IsInterface && type.IsAbstract) continue;

                        Console.WriteLine($"Registering service provider: {type.Name}");
                        var serviceType = implementedType;
                        var implementationType = type;
                        var registerMethod = typeof(ServiceRegistry).GetMethod(nameof(ServiceRegistry.RegisterService)).MakeGenericMethod(serviceType, implementationType);
                        registerMethod.Invoke(serviceRegistry, null);
                    }
                }
            }

            return services;
        }

        public static IModule AddWorkflowsFromModules(this IModule module)
        {
            string pluginsPath = @"C:\Users\Raffee.Parseghian\OneDrive - Octopus Group\Documents\__repos\_test\ElsaServer\ElsaServer\ModulePlugins";
            var loadedAssemblies = LoadAssembliesFromFolder(pluginsPath);

            foreach (var assembly in loadedAssemblies)
            {
                module.AddWorkflowsFrom(assembly);
            }

            return module;
        }

        public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            var serviceRegistry = new ServiceRegistry(services);
            services.AddSingleton(serviceRegistry);
            return services;
        }


        public static List<Assembly> LoadAssembliesFromFolder(string folderPath)
        {
            List<Assembly> loadedAssemblies = new();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return loadedAssemblies;
            }

            foreach (string dll in Directory.GetFiles(folderPath, "*.dll"))
            {
                try
                {
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                    loadedAssemblies.Add(assembly);
                    Console.WriteLine($"Loaded: {dll}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load {dll}: {ex.Message}");
                }
            }

            return loadedAssemblies;
        }
    }
}
   