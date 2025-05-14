using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace MageburgDevDays
{
    internal class SemanticKernelTools
    {
        async void runLLM()
        {
            var ip = "http://127.0.0.1:1234/v1"; //LM Studio
#pragma warning disable SKEXP007
            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion(
                modelId: "phi4-mini-instruct",
        apiKey: "lm-studio",
        endpoint: new Uri(ip)
    );
            var kernel = builder.Build();


            kernel.Plugins.AddFromObject(new WetterPlugin());

            PromptExecutionSettings settings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };


            //var chatPrompt = new ChatHistory();
            //chatPrompt.AddSystemMessage("You are a helpful assistant with some tools available.");
            //chatPrompt.AddUserMessage("was brauche ich in Wien heute für Kleidung draussen?");


            //var chatResponse = await chat.GetChatMessageContentAsync(chatPrompt);
            //var result = await kernel.InvokePromptAsync("Sag mir das Wetter in Wien.", new KernelArguments(settings));
            //var result = await kernel.InvokePromptAsync("Was ziehe ich heute an draussen in  Wien.", new KernelArguments(settings));

            //with some tools available"

            var prompt = @"
<|system|>
You are a helpful assistant with some tools available.
<|tool|>
[
  {
    ""name"": ""WetterPlugin-GetWetter"",
    ""description"": ""Get the current weather for a city as text"",
    ""parameters"": {""Ort"": 
        {""description"": ""city"","" 
        ""type"": ""str"", ""default"": ""London""}
       }
    }
  }
]
<|/tool|>
<|end|>
<|user|>
Brauche ich heute eine Jacke in Wien?
<|end|>
<|assistant|>";
            var result = await kernel.InvokePromptAsync(prompt, new KernelArguments(settings));

            Console.WriteLine($"Chat response: {result}");

        }
        public class WetterPlugin
        {
            [KernelFunction("GetWetter")]
            [Description("Weather details of today for a city as text")]
            [return: Description("weather")]
            public async Task<string> GetWetter([Description("city")] string Ort)
            {
                return $"Das Wetter in {Ort} ist aktuell -10° bei Schneefall."; //und Sonnenschein
            }
        }


    }
}

