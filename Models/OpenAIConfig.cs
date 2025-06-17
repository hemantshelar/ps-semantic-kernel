using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_semantic_kernel.Models;

public class OpenAIConfig
{
    public string ApiKey { get; set; }
    public string Model { get; set; }
}


public class MistralAIConfig
{
    public string ModelId { get; set; }
    public string ApiKey { get; set; }
}
public class GoogleAIConfig
{
    public string ModelId { get; set; }
    public string ApiKey { get; set; }
}
