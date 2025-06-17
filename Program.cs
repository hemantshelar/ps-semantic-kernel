using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ps_semantic_kernel.Models;

namespace ps_semantic_kernel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            ServiceCollection services = new();

            services.AddSingleton<Application>();
            services.AddSingleton(config);

            services.Configure<OpenAIConfig>((x) =>
            {
                x.ApiKey = config["OpenAIConfig:ApiKey"];
                x.Model = config["OpenAIConfig:Model"];
            });


            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<Application>();
            app.Run();






        }
    }
}
