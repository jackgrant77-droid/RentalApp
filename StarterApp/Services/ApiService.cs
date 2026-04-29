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

 
    public async Task<List<Item>> GetNearbyItemsAsync(double latitude, double longitude, double radiusKm)
    {
        var url = $"/items/nearby?lat={latitude}&lon={longitude}&radius={radiusKm}";

        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(json);
        }

        var result = JsonSerializer.Deserialize<ApiNearbyItemsResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Items.Select(dto => new Item
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            DailyRate = dto.DailyRate,
            Category = dto.Category,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude
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
            latitude = item.Latitude,
            longitude = item.Longitude
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

   public async Task UpdateRentalStatusAsync(int rentalId, string status)
{
    await AddAuthHeaderAsync();

    var request = new
    {
        status
    };

    var json = JsonSerializer.Serialize(request);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await _httpClient.PatchAsync($"/rentals/{rentalId}/status", content);
    var responseJson = await response.Content.ReadAsStringAsync();

    if (!response.IsSuccessStatusCode)
    {
        throw new Exception(responseJson);
    }
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

    public async Task<List<Rental>> GetOutgoingRentalsAsync()
    {
        await AddAuthHeaderAsync();

        var response = await _httpClient.GetAsync("/rentals/incoming");
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(json);
        }

        var result = JsonSerializer.Deserialize<ApiRentalsResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result?.Rentals.Select(dto => new Rental
        {
            Id = dto.Id,
            ItemId = dto.ItemId,
            Item = new Item
            {
                Id = dto.ItemId,
                Title = dto.ItemTitle
            },
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = dto.TotalPrice,
            Status = Enum.TryParse<RentalStatus>(
                dto.Status.Replace(" ", ""),
                out var status)
                    ? status
                    : RentalStatus.Requested
        }).ToList() ?? new List<Rental>();
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

    private class ApiNearbyItemsResponse
    {
        public List<ApiNearbyItemDto> Items { get; set; } = new();
    }

    private class ApiNearbyItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public string Category { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
    }

    private class ApiRentalsResponse
    {
        public List<ApiRentalDto> Rentals { get; set; } = new();
    }

    private class ApiRentalDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemTitle { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
    }
}