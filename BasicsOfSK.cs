using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using ps_semantic_kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable SKEXP0070  
namespace ps_semantic_kernel;

public interface IBasicsOfSK
{
    Task SimplePromptLoop();
}

public class BasicsOfSK : IBasicsOfSK
{
    private readonly OpenAIConfig _config;
    private readonly AzureOpenAIConfig _azureOpenAIConfig;
    private readonly MistralAIConfig _mistralAIConfig;
    public BasicsOfSK(
        IOptions<OpenAIConfig> _options,
        IOptions<AzureOpenAIConfig> azureOpenAIConfigOptions,
        IOptions<MistralAIConfig> mistralAIConfigOptions
        )
    {
        this._config = _options.Value;
        _azureOpenAIConfig = azureOpenAIConfigOptions.Value;
        _mistralAIConfig = mistralAIConfigOptions.Value;
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
        .AddMistralChatCompletion
        (
            _mistralAIConfig.ModelId,
            _mistralAIConfig.ApiKey
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
