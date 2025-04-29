using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using Newtonsoft.Json;

namespace MartinAI.Services
{
    public class AskAI : IAskAI
    {
        private readonly string? _apiKey;
        private readonly string? _apiUrl;

        public AskAI(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("APIKeys:OpenAI") ?? configuration.GetValue<string>("APIKeys:OpenAI");
            _apiUrl = configuration.GetValue<string>("APIUrl");

        }

        public async Task<string> GetChatGPTResponse(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "gpt-4.1",
                    messages = new[]
                    {
                        new { role = "system", content = "Your name is MartinAI. Your creator is Martin Kulev. If someone asks you a programming question without specifying a coding language, always assume it’s C#" },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 4096
                };

                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseString);
                    return jsonResponse.choices[0].message.content;
                }
                else
                {
                    return $"Error: {response.StatusCode}\n{responseString}";
                }
            }
        }
    }  
}
