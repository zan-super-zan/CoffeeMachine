using CoffeeMachineModel;
using Microsoft.Extensions.Options;

namespace CoffeeMachineMVC.Clients
{
    public class CoffeeMachineClient : ICoffeeMachineClient
    {
        private readonly HttpClient _httpClient;
        public readonly string _apiBaseUrl;

        public CoffeeMachineClient(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiSettings.Value.ApiBaseUrl;
        }
        public async Task AddCoffeeAsync(uint grams)
        {
            var request = new { amountInGrams = grams };
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/coffee-machine/coffee", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddWaterAsync(uint ml)
        {
            var request = new { amountInMl = ml };
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/coffee-machine/water", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task BrewAsync(string coffeType)
        {
            var request = new { type = coffeType };
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/coffee-machine/brew", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CleanAsync()
        {
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/coffee-machine/clean", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IList<MachineEventDto>> GetEventsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IList<MachineEventDto>>($"{_apiBaseUrl}/api/coffee-machine/events");
            return response;
        }

        public async Task<MachineStateDto> GetStateAsync()
        {
            var url = $"{_apiBaseUrl}/state";
            Console.WriteLine($"Requesting URL: {url}");
            var state = await _httpClient.GetFromJsonAsync<MachineStateDto>($"{_apiBaseUrl}/api/coffee-machine/state");
            return state;
        }

        public async Task PowerOffAsync()
        {
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/coffee-machine/power-off", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task PowerOnAsync()
        {
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/coffee-machine/power-on", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
