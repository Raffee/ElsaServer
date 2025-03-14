using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;
using Elsa.Workflows.Runtime;
using ElsaServer.Workflows;
using Genesis.Common.Core;
using Genesis.Core.WorkflowServer;

namespace ElsaServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServiceRegistry();

            builder.Services.LoadBusinessModules();
            builder.Services.LoadServiceProviders();

            builder.Services.AddElsa(elsa =>
            {
                // Configure Management layer to use EF Core.
                elsa.UseWorkflowManagement(management => management.UseEntityFrameworkCore(ef => ef.UseSqlite()));

                // Configure Runtime layer to use EF Core.
                elsa.UseWorkflowRuntime(runtime => runtime.UseEntityFrameworkCore(ef => ef.UseSqlite()));

                // Default Identity features for authentication/authorization.
                elsa.UseIdentity(identity =>
                {
                    identity.TokenOptions = options => options.SigningKey = "sufficiently-large-secret-signing-key"; // This key needs to be at least 256 bits long.
                    identity.UseAdminUserProvider();
                });

                // Configure ASP.NET authentication/authorization.
                elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());

                // Expose Elsa API endpoints.
                elsa.UseWorkflowsApi();

                // Enable JavaScript workflow expressions.
                elsa.UseJavaScript();

                // Enable C# workflow expressions.
                elsa.UseCSharp();

                // Enable Liquid workflow expressions.
                elsa.UseLiquid();

                // Enable HTTP activities.
                elsa.UseHttp(options => options.ConfigureHttpOptions = httpOptions =>
                {
                    httpOptions.BaseUrl = new Uri("https://localhost:5001");
                    httpOptions.BasePath = "/workflows";
                });
                //elsa.AddWorkflow<GetUser>();

                elsa.UseEmail(email =>
                {
                    email.ConfigureOptions = options =>
                    {
                        options.Host = "localhost";
                        options.Port = 2525;
                    };
                });

                // Use timer activities.
                elsa.UseScheduling();

                // Register custom activities from the application, if any.
                elsa.AddActivitiesFrom<Program>();

                // Register custom workflows from the application, if any.
                elsa.AddWorkflowsFrom<Program>();
                elsa.AddWorkflowsFromModules();
            });

            builder.Services.AddTransient<Mediator>();

            // Configure CORS to allow designer app hosted on a different origin to invoke the APIs.
            builder.Services.AddCors(cors => cors
                .AddDefaultPolicy(policy => policy
                    .AllowAnyOrigin() // For demo purposes only. Use a specific origin instead.
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("x-elsa-workflow-instance-id"))); // Required for Elsa Studio in order to support running workflows from the designer. Alternatively, you can use the `*` wildcard to expose all headers.

            // Add Health Checks.
            builder.Services.AddHealthChecks();

            // Build the web application.
            var app = builder.Build();

            // Configure web application's middleware pipeline.
            app.UseCors();
            app.UseRouting(); // Required for SignalR.
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWorkflowsApi(); // Use Elsa API endpoints.
            app.UseWorkflows(); // Use Elsa middleware to handle HTTP requests mapped to HTTP Endpoint activities.
            //app.UseWorkflowsSignalRHubs(); // Optional SignalR integration. Elsa Studio uses SignalR to receive real-time updates from the server. 

            app.Run();
        }
    }
}
