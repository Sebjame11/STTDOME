using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenAI;

namespace BlockWorkflow.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAI.OpenAI _client;

        public OpenAIService(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];
            _client = new OpenAI.OpenAI(apiKey);
        }

        public async Task<string> GetCompletion(string prompt)
        {
            try
            {
                var completion = await _client.ChatEndpoint.GetCompletionAsync(new ChatRequest
                {
                    Model = "gpt-4",
                    Messages = new[]
                    {
                        new ChatMessage
                        {
                            Role = "user",
                            Content = prompt
                        }
                    }
                });

                return completion.Choices[0].Message.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"OpenAI API error: {ex.Message}");
            }
        }
    }
} 