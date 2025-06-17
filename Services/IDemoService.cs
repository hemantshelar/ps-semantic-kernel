using Microsoft.Extensions.Options;
using ps_semantic_kernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel.Services
{
    public interface IDemoService
    {
        void RunDemo();
    }

    public class DemoService : IDemoService
    {
        private readonly OpenAIConfig _config;

        public DemoService(IOptions<OpenAIConfig> config)
        {
            _config = config.Value;
        }
        public void RunDemo()
        {
            Console.WriteLine("Demo Service is running...");
            // Add your demo logic here
        }
    }
}
