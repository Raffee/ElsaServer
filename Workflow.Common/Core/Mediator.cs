using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Common.Core
{
    public class Mediator
    {
        private readonly ServiceRegistry _serviceRegistry;

        public Mediator(ServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
        }

        public async Task<TResponse?> SendAsync<TRequest, TResponse>(string actionName, TRequest request)
        {
            var actionMethod = _serviceRegistry.GetServiceHandler(actionName);

            if (actionMethod == null || actionMethod.IsStatic)
            {
                throw new InvalidOperationException($"No service handler found for action {actionName}");
            }

            var handler = _serviceRegistry.GetServiceProvider().GetService(actionMethod.DeclaringType);

            if (handler == null)
            {
                throw new InvalidOperationException($"No service handler found for action {actionName}");
            }

            return await (Task<TResponse?>)actionMethod!.Invoke(handler!, [request!]);
        }
    }
}
