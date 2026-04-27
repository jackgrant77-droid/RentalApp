using System.Text.Json;
using StarterApp.Database.Models;
using System.Net.Http.Json;

namespace StarterApp.Services;

public class ApiAuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://set09102-api.b-davison.workers.dev")
    };

    public event EventHandler<bool>? AuthenticationStateChanged;

    public bool IsAuthenticated { get; private set; }

    public User? CurrentUser { get; private set; }

    public List<string> CurrentUserRoles { get; private set; } = new();

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/auth/token", new
            {
                email,
                password
            });

            if (!response.IsSuccessStatusCode)
            {
                return new AuthenticationResult(false, "Invalid login");
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TokenResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
            {
                return new AuthenticationResult(false, "Invalid response");
            }

            // store token
            await SecureStorage.SetAsync("jwt_token", result.Token);

            // minimal user (API doesn't return full user here)
            CurrentUser = new User
            {
                Id = result.UserId,
                Email = email,
                FirstName = "User",
                LastName = ""
            };

            CurrentUserRoles = new List<string> { "User" };

            IsAuthenticated = true;
            AuthenticationStateChanged?.Invoke(this, true);

            return new AuthenticationResult(true, "Login successful");
        }
        catch (Exception ex)
        {
            return new AuthenticationResult(false, ex.Message);
        }
    }

    public Task<AuthenticationResult> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        return Task.FromResult(new AuthenticationResult(false, "Register not implemented yet"));
    }

    public async Task LogoutAsync()
    {
        await SecureStorage.SetAsync("jwt_token", "");
        IsAuthenticated = false;
        CurrentUser = null;
        AuthenticationStateChanged?.Invoke(this, false);
    }

    public bool HasRole(string roleName) => CurrentUserRoles.Contains(roleName);

    public bool HasAnyRole(params string[] roleNames) =>
        roleNames.Any(r => CurrentUserRoles.Contains(r));

    public bool HasAllRoles(params string[] roleNames) =>
        roleNames.All(r => CurrentUserRoles.Contains(r));

    public Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        return Task.FromResult(false);
    }

    private class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public int UserId { get; set; }
    }
}