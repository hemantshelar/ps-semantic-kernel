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
    public BasicsOfSK(IOptions<OpenAIConfig> _options)
    {
        this._config = _options.Value;
    }

    public async Task SimplePromptLoop()
    {
        // gpt-4.1
        // gpt-4.1-nano" 

        Kernel kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(_config.Model, _config.ApiKey)
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
