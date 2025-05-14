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
    public class SemanticKernellokalVision
    {
#pragma warning disable SKEXP0070


       public async void runLLM()
        {
            var modelPath = @"C:\stateof\Phi-3.5-mini-instruct-onnx\cpu_and_mobile\cpu-int4-awq-block-128-acc-level-4";


            var builder = Kernel.CreateBuilder();
            builder.AddOnnxRuntimeGenAIChatCompletion("phi-3-vision", modelPath);
            var kernel = builder.Build();

            var chat = kernel.GetRequiredService<IChatCompletionService>();
            var history = new ChatHistory();

            var PDFImagePath = "C:\\stateof\\imgs\\1000040581.png";

            var collectionItems = new ChatMessageContentItemCollection
{
    new TextContent("you are a OCR Tool. This is a  receipt, give me IBAN from image as Json formated Data "),
    new ImageContent( new Uri(PDFImagePath))

};
            history.AddUserMessage(collectionItems);

            Console.Write($"Phi3: ");
            var result = await chat.GetChatMessageContentsAsync(history);
            Console.WriteLine(result[^1].Content);

            Console.WriteLine("");

        }

    }
}
