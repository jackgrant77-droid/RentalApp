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
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    private async Task AddAuthHeaderAsync()
    {
        var token = await SecureStorage.GetAsync("jwt_token");

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        var response = await _httpClient.GetAsync("/items");
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(json);
        }

        var result = JsonSerializer.Deserialize<ApiItemsResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Items.Select(dto => new Item
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            DailyRate = dto.DailyRate,
            Category = dto.Category
        }).ToList() ?? new List<Item>();
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        await AddAuthHeaderAsync();

        var request = new
        {
            title = item.Title,
            description = item.Description,
            dailyRate = item.DailyRate,
            categoryId = 1,
            latitude = 55.9533,
            longitude = -3.1883
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/items", content);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseJson);
        }

        var dto = JsonSerializer.Deserialize<ApiItemDto>(responseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

      return new Item
    {
        Id = dto?.Id ?? 0,
        Title = dto?.Title ?? item.Title,
        Description = dto?.Description ?? item.Description,
        DailyRate = dto?.DailyRate ?? item.DailyRate,
        Category = dto?.Category ?? item.Category
    };

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
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseJson);
        }
    }

    private class ApiItemsResponse
    {
        public List<ApiItemDto> Items { get; set; } = new();
    }


    private class ApiItemDto
    {

    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal DailyRate { get; set; }

    public string Category { get; set; } = string.Empty;

    public int OwnerId { get; set; }

    }

}