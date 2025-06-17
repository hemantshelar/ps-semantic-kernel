using Microsoft.Extensions.Options;
using ps_semantic_kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel
{
    public class Application
    {
        private readonly OpenAIConfig _config;
        public Application(IOptions<OpenAIConfig> config)
        {
            _config = config.Value;

        }


        public void Run()
        {
            Console.WriteLine("Application is running...");
            // Add your application logic here
        }
    }
}
