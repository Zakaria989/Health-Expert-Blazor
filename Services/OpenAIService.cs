using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Pages;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace Health_expert_B.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ChatOptions> _options;
        private readonly IOptions<Dalle2Options>  _imageOptions;

        public OpenAIService(HttpClient httpClient, IOptions<ChatOptions> options, IOptions<Dalle2Options> imageOptions)
        {
            _httpClient = httpClient;
            _options = options;
            _imageOptions = imageOptions;
        }

        public async Task<Message> CreateChatCompletion(List<Message> messages) 
        {
            var request = new {model = _options.Value.GptModel, messages = messages.ToArray()};
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Value.ApiKey);
            
            var response = await _httpClient.PostAsJsonAsync(_options.Value.ApiUrl, request);
            response.EnsureSuccessStatusCode();

            var chatCompletionResponse = await response.Content.ReadFromJsonAsync<ChatBotResponse>();
            return chatCompletionResponse?.choices.First().message;
        } 
        public async Task<string?> CreateImageFromPrompt(string prompt)
        {
            var request = new Dalle2Request { Prompt = prompt, N = 1, Size = "256x256" };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _imageOptions.Value.ApiKey);

            var response = await _httpClient.PostAsJsonAsync(_imageOptions.Value.ImageUrl, request);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Add logging to see the content of the response when there's an error
                Console.WriteLine($"Failed to create image. Response status code: {response.StatusCode}, Content: {content}");
                // You can also throw an exception here or handle the error accordingly
                return null;
            }

            var imageGenerationResponse = await response.Content.ReadFromJsonAsync<ImageGenerationResponse>();
            return imageGenerationResponse?.data?.FirstOrDefault()?.url;
        }


    }
}
