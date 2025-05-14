using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace MageburgDevDays
{
    public class SemanticKernelOllama
    {
       async void runLLM()
        {
            var ip = "http://1.1.1.1:11434/";
            //your ollama public server

#pragma warning disable SKEXP0001

#pragma warning disable SKEXP0070

            IKernelBuilder builderllama = Kernel.CreateBuilder()
                .AddOllamaChatCompletion("phi4-mini:3.8b-fp16", new Uri(ip));

            Kernel kernelLlama = builderllama.Build();



            var result = await kernelLlama.InvokePromptAsync(
                "wieviel ist die summe aus  1 und eins . Antowrte als JSON");


            Console.WriteLine(result);

        }
    }

}
