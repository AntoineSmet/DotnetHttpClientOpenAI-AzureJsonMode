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
            var apiKey = "Remplacez par votre clé API OpenAI";
            var endpoint = "https://api.openai.com/v1/chat/completions";

            var requestPayload = @"
            {
                ""model"": ""gpt-35-turbo-1106"",
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
                ""functions"": [
                    {
                        ""name"": ""get_current_weather"",
                        ""description"": ""Get the current weather in a given location"",
                        ""parameters"": {
                            ""type"": ""object"",
                            ""properties"": {
                                ""location"": {
                                    ""type"": ""string"",
                                    ""description"": ""The city and state, e.g. San Francisco, CA""
                                },
                                ""unit"": {
                                    ""type"": ""string"",
                                    ""enum"": [""celsius"", ""fahrenheit""]
                                }
                            },
                            ""required"": [""location""]
                        }
                    }
                ],
                ""function_call"": ""auto"",
                ""temperature"": 0.7
            }
            ";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var content = new StringContent(requestPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Réponse de l'API OpenAI :");
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
