using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StarterApp.Database.Models;
namespace StarterApp.Services;
/// <summary>
/// Handles all communication between the MAUI app and the external coursework API.
/// This keeps HTTP/API logic separate from ViewModels.
/// </summary>
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
   /// <summary>
   /// Adds the stored JWT token to authenticated API requests.
   /// </summary>
   private async Task AddAuthHeaderAsync()
   {
       var token = await SecureStorage.GetAsync("jwt_token");
       if (!string.IsNullOrWhiteSpace(token))
       {
           _httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", token);
       }
   }
   /// <summary>
   /// Retrieves all available items from the API.
   /// </summary>
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
   /// <summary>
   /// Retrieves items near the user's location using latitude, longitude and radius.
   /// </summary>
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
   /// <summary>
   /// Creates a new item listing through the API.
   /// </summary>
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
   /// <summary>
   /// Updates a rental status, such as Approved, Rejected, Returned or Completed.
   /// </summary>
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
   /// <summary>
   /// Creates a rental request for a selected item.
   /// </summary>
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
   /// <summary>
   /// Retrieves rentals sent by the current user.
   /// </summary>
   public async Task<List<Rental>> GetOutgoingRentalsAsync()
   {
       await AddAuthHeaderAsync();
       var response = await _httpClient.GetAsync("/rentals/outgoing");
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
   /// <summary>
   /// Submits a review for a completed rental.
   /// </summary>
   public async Task SubmitReviewAsync(int rentalId, int rating, string comment)
   {
       await AddAuthHeaderAsync();
       var request = new
       {
           rentalId,
           rating,
           comment
       };
       var json = JsonSerializer.Serialize(request);
       var content = new StringContent(json, Encoding.UTF8, "application/json");
       var response = await _httpClient.PostAsync("/reviews", content);
       var responseJson = await response.Content.ReadAsStringAsync();
       if (!response.IsSuccessStatusCode)
       {
           throw new Exception(responseJson);
       }
   }
   /// <summary>
   /// Retrieves reviews for a specific item.
   /// </summary>
   public async Task<List<Review>> GetItemReviewsAsync(int itemId)
   {
       var response = await _httpClient.GetAsync($"/items/{itemId}/reviews");
       var json = await response.Content.ReadAsStringAsync();
       if (!response.IsSuccessStatusCode)
       {
           throw new Exception(json);
       }
       var result = JsonSerializer.Deserialize<ApiReviewsResponse>(json, new JsonSerializerOptions
       {
           PropertyNameCaseInsensitive = true
       });
       return result?.Reviews.Select(dto => new Review
       {
           Id = dto.Id,
           ReviewerId = dto.ReviewerId,
           ReviewerName = dto.ReviewerName,
           Rating = dto.Rating,
           Comment = dto.Comment,
           CreatedAt = dto.CreatedAt
       }).ToList() ?? new List<Review>();
   }
   /// <summary>
   /// DTO used to match the API response structure for item lists.
   /// </summary>
   private class ApiItemsResponse
   {
       public List<ApiItemDto> Items { get; set; } = new();
   }
   /// <summary>
   /// DTO used to safely deserialize item data returned by the API.
   /// </summary>
   private class ApiItemDto
   {
       public int Id { get; set; }
       public string Title { get; set; } = string.Empty;
       public string Description { get; set; } = string.Empty;
       public decimal DailyRate { get; set; }
       public string Category { get; set; } = string.Empty;
       public int OwnerId { get; set; }
   }
   /// <summary>
   /// DTO used to match the API response for nearby item searches.
   /// </summary>
   private class ApiNearbyItemsResponse
   {
       public List<ApiNearbyItemDto> Items { get; set; } = new();
   }
   /// <summary>
   /// DTO for items returned from the nearby search endpoint.
   /// </summary>
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
   /// <summary>
   /// DTO used to match the API response for rental lists.
   /// </summary>
   private class ApiRentalsResponse
   {
       public List<ApiRentalDto> Rentals { get; set; } = new();
   }
   /// <summary>
   /// DTO for rental data returned by the API.
   /// </summary>
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
   /// <summary>
   /// DTO used to match the API response for review lists.
   /// </summary>
   private class ApiReviewsResponse
   {
       public List<ApiReviewDto> Reviews { get; set; } = new();
   }
   /// <summary>
   /// DTO for review data returned by the API.
   /// </summary>
   private class ApiReviewDto
   {
       public int Id { get; set; }
       public int ReviewerId { get; set; }
       public string ReviewerName { get; set; } = string.Empty;
       public int Rating { get; set; }
       public string Comment { get; set; } = string.Empty;
       public DateTime CreatedAt { get; set; }
   }
}