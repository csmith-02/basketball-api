using System.Text.Json;
using FullCourtInsights.Models;
using Microsoft.Extensions.Options;

namespace FullCourtInsights.Services
{
    public class BasketballApiService
    {

        private readonly ApiSettings _apiSettings;

        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _options;
        public BasketballApiService(IOptions<ApiSettings> apiSettings, HttpClient httpClient)
        {
            _apiSettings = apiSettings.Value;

            _options = new()
            {
                PropertyNameCaseInsensitive = true,
            };

            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://v2.nba.api-sports.io");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v2.nba.api-sports.io");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiSettings.BasketballApiKey);
        }

        public async Task<IEnumerable<PlayerRequest>> GetPlayersByName(string name)
        {
            var response = await _httpClient.GetStringAsync($"players?name={name}");

            IEnumerable<PlayerRequest>? p;

            try
            {
                var apiResponse = JsonSerializer.Deserialize<PlayersApiResponse>(response, _options);
                p = apiResponse?.Response?.ToList() ?? [];

            } catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                p = [];
            }

            return p;
        }

        public async Task<IEnumerable<PlayerRequest>> GetPlayerById(string id)
        {
            var response = await _httpClient.GetStringAsync($"players?id={id}");

            IEnumerable<PlayerRequest>? p;

            try
            {
                var apiResponse = JsonSerializer.Deserialize<PlayersApiResponse>(response, _options);
                p = apiResponse?.Response?.ToList() ?? [];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                p = [];
            }

            return p;
        }

        public async Task<IEnumerable<PlayerStatRequest>> GetPlayerStats(string id, string season)
        {
            var response = await _httpClient.GetStringAsync($"players/statistics?id={id}&season={season}");

            IEnumerable<PlayerStatRequest>? p;

            try
            {
                var apiResponse = JsonSerializer.Deserialize<StatApiResponse>(response, _options);
                p = apiResponse?.Response?.ToList() ?? [];
            } catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                p = [];
            }

            return p;
        }
    }
}
