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

            services.AddSingleton<IBasicsOfSK, BasicsOfSK>();

            services.AddSingleton<Application>();
            services.AddSingleton(config);

            services.Configure<OpenAIConfig>((x) =>
            {
                x.ApiKey = config["OpenAIConfig:ApiKey"];
                x.Model = config["OpenAIConfig:Model"];
            });

            services.Configure<AzureOpenAIConfig>((x) =>
            {
                x.ApiKey = config["AzureOpenAIConfig:ApiKey"];
                x.Endpoint = config["AzureOpenAIConfig:Endpoint"];
                x.DeploymentName = config["AzureOpenAIConfig:DeploymentName"];
            });

            services.Configure<MistralAIConfig>((options) =>
            {
                options.ApiKey = config["MistralAIConfig:ApiKey"];
                options.ModelId = config["MistralAIConfig:ModelId"];
            });

            services.Configure<GoogleAIConfig>((options) =>
            {
                options.ApiKey = config["GoogleAIConfig:ApiKey"];
                options.ModelId = config["GoogleAIConfig:ModelId"];
            });


            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<Application>();
            var basicsOfSK = serviceProvider.GetRequiredService<IBasicsOfSK>();
            //app.Run();
            basicsOfSK.SimplePromptLoop().GetAwaiter().GetResult();






        }
    }
}
