using System.Net.Http.Headers;
using System.Net.Http.Json;
using TikTask2._0.Web.Models;

namespace TikTask2._0.Web.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private string? _token;

    public event Action? OnAuthStateChanged;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);
    public string? Username { get; private set; }
    public string? Role { get; private set; }

    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        var apiUrl = _configuration["ApiUrl"] ?? "https://localhost:7001";
        _httpClient.BaseAddress = new Uri($"{apiUrl}/api/");
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", request);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                if (authResponse != null)
                {
                    SetAuth(authResponse);
                    return true;
                }
            }
        }
        catch
        {
            // Handle error
        }
        return false;
    }

    public async Task<bool> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                if (authResponse != null)
                {
                    SetAuth(authResponse);
                    return true;
                }
            }
        }
        catch
        {
            // Handle error
        }
        return false;
    }

    public void Logout()
    {
        _token = null;
        Username = null;
        Role = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        OnAuthStateChanged?.Invoke();
    }

    public async Task<List<TaskResponse>> GetTasksAsync()
    {
        try
        {
            var tasks = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("tasks");
            return tasks ?? new List<TaskResponse>();
        }
        catch
        {
            return new List<TaskResponse>();
        }
    }

    public async Task<List<TaskResponse>> GetAllTasksAsync()
    {
        try
        {
            var tasks = await _httpClient.GetFromJsonAsync<List<TaskResponse>>("tasks/all");
            return tasks ?? new List<TaskResponse>();
        }
        catch
        {
            return new List<TaskResponse>();
        }
    }

    public async Task<bool> CreateTaskAsync(TaskRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("tasks", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateTaskAsync(int id, TaskRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"tasks/{id}", request);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ToggleTaskCompleteAsync(int id)
    {
        try
        {
            var response = await _httpClient.PatchAsync($"tasks/{id}/complete", null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"tasks/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private void SetAuth(AuthResponse authResponse)
    {
        _token = authResponse.Token;
        Username = authResponse.Username;
        Role = authResponse.Role;
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _token);
        OnAuthStateChanged?.Invoke();
    }
}
