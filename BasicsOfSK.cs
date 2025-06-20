using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.MistralAI;
using Microsoft.SemanticKernel.TextToImage;
using ps_semantic_kernel.Models;
using ps_semantic_kernel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable SKEXP0070  
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
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
    private readonly GoogleAIConfig _googleAIConfig;
    public BasicsOfSK(
        IOptions<OpenAIConfig> _options,
        IOptions<AzureOpenAIConfig> azureOpenAIConfigOptions,
        IOptions<MistralAIConfig> mistralAIConfigOptions,
        IOptions<GoogleAIConfig> googleAIConfigOptions
        )
    {
        this._config = _options.Value;
        _azureOpenAIConfig = azureOpenAIConfigOptions.Value;
        _mistralAIConfig = mistralAIConfigOptions.Value;
        _googleAIConfig = googleAIConfigOptions.Value;
    }

    public async Task SimplePromptLoop()
    {
        var builder = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(_config.Model, _config.ApiKey)
        .AddAzureOpenAIChatCompletion
        (
            _azureOpenAIConfig.DeploymentName,
            _azureOpenAIConfig.Endpoint,
            _azureOpenAIConfig.ApiKey
        )
        .AddMistralChatCompletion
        (
            //SKEXP0070 - AddMistralChatCompletion is for evaluation purpose only.
            _mistralAIConfig.ModelId,
            _mistralAIConfig.ApiKey
        )
        .AddGoogleAIGeminiChatCompletion
        (
            //SKEXP0070 - AddGoogleAIGeminiChatCompletion is for evaluation purpose only.
            _googleAIConfig.ModelId,
            _googleAIConfig.ApiKey
        )
        .AddOpenAITextToImage
        (
            //SKEXP0010 - AddOpenAITextToImage is for evaluation purpose only.
            _config.ApiKey
        )
        .AddAzureOpenAITextToImage
        (
           //SKEXP0010 - AddAzureOpenAITextToImage is for evaluation purpose only.
           _azureOpenAIConfig.DeploymentName,
           _azureOpenAIConfig.Endpoint,
           _azureOpenAIConfig.ApiKey
        );

        builder.Services.AddSingleton<IAIServiceSelector, AIServiceSelector>();
        Kernel kernel = builder.Build();

        string input = string.Empty;
        /*while (input != "exit")
        {
            Console.Write("Enter your prompt (type 'exit' to quit): ");
            input = Console.ReadLine();
            if (input.ToLower() == "exit")
                break;
            var result = await kernel.InvokePromptAsync(input);
            Console.WriteLine($"Response: {result}");
        }*/

        //SKEXP0001 - ITextToImageService evaluation purpose only.
        var allTextToImageServices = kernel.GetAllServices<ITextToImageService>().ToList();
        ITextToImageService? selectedService = allTextToImageServices.FirstOrDefault();
        Console.WriteLine($"Service being used it : {selectedService.GetType().ToString()}");
        var dim = 1024;
        var x = "Indian man near chicken coop";
        var prompt = $"A father, explaing her teenager daughter how to drive a car with the help of toy car in order to explain how to stay in right lane while driving.";
        prompt = "A children's book drawing of a veterinarian using a stethoscope to listen to the heartbeat of a baby otter.";
        var image = selectedService.GenerateImageAsync(prompt, dim, dim).GetAwaiter().GetResult();
        Console.WriteLine($"Image generated with dimensions: {image}"); ;
        Console.WriteLine("Image generated successfully. You can save it or display it as needed.");

    }
}
