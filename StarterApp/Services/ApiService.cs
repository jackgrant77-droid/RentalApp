using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StarterApp.Database.Models;

namespace StarterApp.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    private const string BaseUrl = "https://set09102-api.b-davison.workers.dev";

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(BaseUrl);
    }

    private async Task AddAuthHeaderAsync()
    {
        var token = await SecureStorage.GetAsync("jwt_token");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        var response = await _httpClient.GetAsync("/items");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ApiItemsResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Items ?? new List<Item>();
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        await AddAuthHeaderAsync();

        var json = JsonSerializer.Serialize(item);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/items", content);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Item>(responseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task RequestRentalAsync(int itemId, DateTime startDate, DateTime endDate)
    {
        await AddAuthHeaderAsync();

        var request = new
        {
            itemId,
            startDate = startDate.ToString("yyyy-MM-dd"),
            endDate = endDate.ToString("yyyy-MM-dd")
        };

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/rentals", content);

        response.EnsureSuccessStatusCode();
    }

    private class ApiItemsResponse
    {
        public List<Item> Items { get; set; } = new();
    }
}