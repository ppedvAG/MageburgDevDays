using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace MageburgDevDays
{
    internal class SemanticKernellokalVision
    {

        void runLLM()
        {
            var modelPath = @"C:\stateof\phi-3\Phi-3-vision-128k-instruct-onnx-cpu\cpu-int4-rtn-block-32-acc-level-4";


            var builder = Kernel.CreateBuilder();
            builder.AddOnnxRuntimeGenAIChatCompletion(modelPath: modelPath);
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
