using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MinicabitMcp.Models;

namespace MinicabitMcp.Services;

public class MinicabitApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://api.minicabit.com/v1";
    private readonly string? _apiKey;

    public MinicabitApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Minicabit:ApiKey"];
        
        if (!string.IsNullOrEmpty(_apiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("X-Minicabit-ApiKey-ID", _apiKey);
        }
    }

    public async Task<QuotesResponse?> GetQuotesAsync(QuoteRequest request, CancellationToken cancellationToken = default)
    {
        var query = BuildQueryString(new Dictionary<string, object?>
        {
            ["fromProperty"] = request.FromProperty,
            ["toProperty"] = request.ToProperty,
            ["pickUpDate"] = request.PickUpDate,
            ["pickUpTime"] = request.PickUpTime,
            ["numOfPassengers"] = request.NumOfPassengers,
            ["tripType"] = request.TripType,
            ["returnDate"] = request.ReturnDate,
            ["returnTime"] = request.ReturnTime,
            ["luggage"] = request.Luggage,
            ["vias"] = request.Vias
        });

        var response = await _httpClient.GetAsync($"{_baseUrl}/quotes{query}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<QuotesResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<RouteComparisonResponse?> CheckRouteAsync(CheckRouteRequest request, CancellationToken cancellationToken = default)
    {
        var query = BuildQueryString(new Dictionary<string, object?>
        {
            ["fromProperty"] = request.FromProperty,
            ["toProperty"] = request.ToProperty,
            ["vias"] = request.Vias
        });

        var response = await _httpClient.GetAsync($"{_baseUrl}/quotes/checkRoute{query}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<RouteComparisonResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<BookingResponse[]?> CreateBookingAsync(BookingRequest request, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/bookings/", content, cancellationToken);
        
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<BookingResponse[]>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private static string BuildQueryString(Dictionary<string, object?> parameters)
    {
        var query = new StringBuilder("?");
        var first = true;

        foreach (var kvp in parameters)
        {
            if (kvp.Value == null) continue;

            if (!first) query.Append("&");
            query.Append($"{kvp.Key}={Uri.EscapeDataString(kvp.Value.ToString() ?? "")}");
            first = false;
        }

        return query.ToString();
    }
} 