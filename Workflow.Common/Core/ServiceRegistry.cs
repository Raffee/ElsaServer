using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Common.Core
{
    public class ServiceRegistry
    {
        private readonly Dictionary<string, MethodInfo> _serviceActions = new();
        private readonly IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider;

        public ServiceRegistry(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void RegisterService<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            var serviceType = typeof(TService);
            var methods = serviceType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(ServiceActionAttribute), false).Any());

            foreach (var method in methods)
            {
                var actionNameAttribute = (ServiceActionAttribute)method.GetCustomAttribute(typeof(ServiceActionAttribute));

                if (actionNameAttribute == null)
                {
                    throw new InvalidOperationException($"Method {method.Name} does not have a ServiceActionAttribute");
                }

                _serviceActions.Add(actionNameAttribute.Action, method);
            }

            _serviceCollection.AddTransient<TService, TImplementation>();
            _serviceProvider = _serviceCollection.BuildServiceProvider(); // Rebuild the service provider
        }

        public MethodInfo? GetServiceHandler(string actionName)
        {
            return _serviceActions.TryGetValue(actionName, out var method) ? method : null;
        }

        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}
