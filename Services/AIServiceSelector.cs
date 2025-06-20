using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel.Services;

public class AIServiceSelector : IAIServiceSelector
{
    bool IAIServiceSelector.TrySelectAIService<T>(Kernel kernel, KernelFunction function, KernelArguments arguments, out T? service, out PromptExecutionSettings? serviceSettings) where T : class
    {
        var result = kernel.Services.GetServices(typeof(T));
        service = result.ToArray()[3] as T;
        serviceSettings = new();
        return true;
    }
}
