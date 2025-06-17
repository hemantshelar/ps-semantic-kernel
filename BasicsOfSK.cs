using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using ps_semantic_kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel
{
    public class BasicsOfSK
    {
        public BasicsOfSK(IOptions<OpenAIConfig> _options)
        {

        }

        public async Task SimplePromptLoop()
        {
            // gpt-4.1
            // gpt-4.1-nano" 

            Kernel kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion("gpt-4.1-nano", "")
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
}
