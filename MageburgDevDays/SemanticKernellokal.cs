using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Onnx;

namespace MageburgDevDays
{
    public class SemanticKernellokal
    {
        void runLLM()
        {

#pragma warning disable SKEXP0070
     var modelPath = @"c:\stateof\Phi-4-mini-instruct-onnx\cpu_and_mobile\cpu-int4-rtn-block-32-acc-level-4\";

            var builder = Kernel.CreateBuilder();
            builder.AddOnnxRuntimeGenAIChatCompletion(modelId: "phi4-mini", modelPath: modelPath);
            var kernel = builder.Build();

            var chat = kernel.GetRequiredService<IChatCompletionService>();
            var history = new ChatHistory();

            while (true)
            {
                Console.Write("Q: ");
                var userQ = Console.ReadLine();
                if (string.IsNullOrEmpty(userQ))
                {
                    break;
                }
                history.AddUserMessage(userQ);

                Console.Write($"Phi4: ");
                var response = "";
                var result = chat.GetStreamingChatMessageContentsAsync(history);
                await foreach (var message in result)
                {
                    Console.Write(message.Content);
                    response += message.Content;
                }
                history.AddAssistantMessage(response);
                Console.WriteLine("");
            }

          
        }
    }
