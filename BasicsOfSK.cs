using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using ps_semantic_kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel;

public interface IBasicsOfSK
{
    Task SimplePromptLoop();
}

public class BasicsOfSK : IBasicsOfSK
{
    private readonly OpenAIConfig _config;
    private readonly AzureOpenAIConfig _azureOpenAIConfig;
    public BasicsOfSK(
        IOptions<OpenAIConfig> _options,
        IOptions<AzureOpenAIConfig> azureOpenAIConfigOptions
        )
    {
        this._config = _options.Value;
        _azureOpenAIConfig = azureOpenAIConfigOptions.Value;
    }

    public async Task SimplePromptLoop()
    {
        Kernel kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(_config.Model, _config.ApiKey)
        .AddAzureOpenAIChatCompletion
        (
            _azureOpenAIConfig.DeploymentName,
            _azureOpenAIConfig.Endpoint,
            _azureOpenAIConfig.ApiKey
        )
        .Build();

        string input = string.Empty;
        while (input != "exit")
        {
            Console.Write("Enter your prompt (type 'exit' to quit): ");
            input = Console.ReadLine();
            if (input.ToLower() == "exit")
                break;
            var result = await kernel.InvokePromptAsync(input);
            Console.WriteLine($"Response: {result}");
        }
    }
}
