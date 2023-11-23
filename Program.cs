using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VotreNamespace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SendOpenAIRequest();
        }

        static async Task SendOpenAIRequest()
        {
            var ApiKey = "{key}";
            var endpoint = "{endpoint}/openai/deployments/{modele}/chat/completions?api-version=2023-12-01-preview";

            var requestPayload = @"
            {

                ""response_format"": {""type"": ""json_object""},
                ""messages"": [
                    {
                        ""role"": ""system"",
                        ""content"": ""You are a helpful assistant that extracts data and returns it in JSON format.""
                    },
                    {
                        ""role"": ""user"",
                        ""content"": ""What is the weather like in Boston?""
                    }
                ],
                ""temperature"": 0.7
            }
            ";

            using (var httpClient = new HttpClient())
            {
               httpClient.DefaultRequestHeaders.Add("api-key", ApiKey);

                var content = new StringContent(requestPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Réponse de l'API Azure OpenAI :");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"Erreur : {response.StatusCode}");
                }
            }
        }
    }
}
