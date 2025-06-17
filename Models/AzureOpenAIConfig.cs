using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel.Models;

public class AzureOpenAIConfig
{
    public string DeploymentName { get; set; }
    public string Endpoint { get; set; }
    public string ApiKey { get; set; }
}
