using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Threading.Tasks;

namespace MageburgDevDays
{
    internal class SemanticKernelLmStudio
    {
       async void runLLM()
        {

            var ip = "http://127.0.0.1:1234/v1"; //LM Studio
#pragma warning disable SKEXP007

#pragma warning disable SKEXP0070



            //beide Varianten möglich
            //var chat = kernel.GetChatCompletionService("gpt-4");
            //var chatPrompt = kernel.CreateNewChat();
            //chatPrompt.AppendSystemMessage("You are a helpful assistant.");
            //chatPrompt.AppendUserMessage("What is the capital of France?");
            //var chatResponse = await chat.GenerateMessageAsync(chatPrompt);
            //Console.WriteLine($"Chat response: {chatResponse}");


            // Erstelle den Kernel mit lokalem OpenAI-kompatiblen Modell (z. B. Phi-4 über LM Studio)
            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion(
                modelId: "phi4-mini-instruct",        // Modellname wie in LM Studio sichtbar
                apiKey: "lm-studio",                  // Pseudo-API-Key (wird bei LM Studio nicht geprüft)
                endpoint: new Uri(ip)
            );
            var kernel = builder.Build();

            var chat = kernel.GetRequiredService<IChatCompletionService>();

            var chatPrompt = new ChatHistory();
            chatPrompt.AddSystemMessage("You are a helpful assistant.");
            chatPrompt.AddUserMessage("What is the capital of France?");
            // Antwort generieren
            var chatResponse = await chat.GetChatMessageContentAsync(chatPrompt);
            Console.WriteLine($"Chat response: {chatResponse.Content}");


        }
    }
}
