using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntimeGenAI;

namespace MageburgDevDays
{
    internal class MLohneSemanticKernelLokal
    {
        void runLLM()
        {

        
            // Phi4
            var modelPath = @"C:\stateof\Phi-4-mini-instruct-onnx\cpu_and_mobile\cpu-int4-rtn-block-32-acc-level-4\";

            var systemPrompt = "You are an AI assistant that helps people find information. Answer questions using a direct style. Do not share more information that the requested by the users.";

            var model = new Model(modelPath);
            var tokenizer = new Tokenizer(model);

            Console.WriteLine(@"Phi4:");

            
            while (true)
            {
                Console.WriteLine();
                Console.Write(@"Q: ");
                var userQ = Console.ReadLine();
                if (string.IsNullOrEmpty(userQ))
                {
                    Console.WriteLine("Bye!");
                    break;
                }

                Console.Write("Phi4: ");
                var fullPrompt = $"<|system|>{systemPrompt}<|end|><|user|>{userQ}<|end|><|assistant|>";
                var tokens = tokenizer.Encode(fullPrompt);

                var generatorParams = new GeneratorParams(model);
                var sequences = tokenizer.Encode(fullPrompt);

                generatorParams.SetSearchOption("max_length", 2048);
                generatorParams.SetSearchOption("past_present_share_buffer", false);
                using var tokenizerStream = tokenizer.CreateStream();
                var generator = new Generator(model, generatorParams);

                generator.AppendTokens(tokens[0].ToArray());
                while (!generator.IsDone())
                {
                    generator.GenerateNextToken();
                    var output = tokenizerStream.Decode(generator.GetSequence(0)[^1]);
                    Console.Write(output);
                }
                Console.WriteLine();
            }
        }
    }
}
