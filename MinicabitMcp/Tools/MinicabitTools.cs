using System.ComponentModel;
using System.Text.Json;
using MinicabitMcp.Models;
using MinicabitMcp.Services;
using ModelContextProtocol.Server;

namespace MinicabitMcp.Tools;

[McpServerToolType]
public class MinicabitTools
{
    [McpServerTool]
    [Description("Get quotes for a taxi trip between two locations. Supports single, return, and split trip types.")]
    public static async Task<string> GetQuotes(
        MinicabitApiService apiService,
        [Description("Full address with postcode for pickup location (e.g., 'Elgin Rail Station (IV30 1QP)')")] string fromProperty,
        [Description("Full address with postcode for destination (e.g., 'Dr Grays Hospital - Elgin(IV30 1SN)')")] string toProperty,
        [Description("Pick up date in YYYY-MM-DD format")] string pickUpDate,
        [Description("Pick up time in HH:MM format (24-hour)")] string pickUpTime,
        [Description("Number of passengers")] int numOfPassengers,
        [Description("Type of trip: Single, Return, or Split")] string tripType,
        [Description("Return date in YYYY-MM-DD format (required for Return and Split trips)")] string? returnDate = null,
        [Description("Return time in HH:MM format (required for Return and Split trips)")] string? returnTime = null,
        [Description("Luggage information as JSON string")] string? luggage = null,
        [Description("Array of via addresses as JSON string")] string? vias = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new QuoteRequest
            {
                FromProperty = fromProperty,
                ToProperty = toProperty,
                PickUpDate = pickUpDate,
                PickUpTime = pickUpTime,
                NumOfPassengers = numOfPassengers,
                TripType = tripType,
                ReturnDate = returnDate,
                ReturnTime = returnTime,
                Luggage = luggage,
                Vias = vias
            };

            var response = await apiService.GetQuotesAsync(request, cancellationToken);
            
            if (response == null)
            {
                return "No quotes found for the specified trip.";
            }

            return JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        catch (HttpRequestException ex)
        {
            return $"API request failed: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error getting quotes: {ex.Message}";
        }
    }

    [McpServerTool]
    [Description("Check for cheaper route options when planning a trip with multiple stops (vias).")]
    public static async Task<string> CheckRoute(
        MinicabitApiService apiService,
        [Description("Full address with postcode for starting location")] string fromProperty,
        [Description("Full address with postcode for destination")] string toProperty,
        [Description("Array of via address objects as JSON string with properties: no, property, postCode, latitude, longitude")] string vias,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new CheckRouteRequest
            {
                FromProperty = fromProperty,
                ToProperty = toProperty,
                Vias = vias
            };

            var response = await apiService.CheckRouteAsync(request, cancellationToken);
            
            if (response == null)
            {
                return "No route comparison data available.";
            }

            return JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        catch (HttpRequestException ex)
        {
            return $"API request failed: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error checking route: {ex.Message}";
        }
    }

    [McpServerTool]
    [Description("Create a booking for a taxi trip. This endpoint creates actual bookings in the Minicabit system.")]
    public static async Task<string> CreateBooking(
        MinicabitApiService apiService,
        [Description("Complete booking request as JSON string containing tripInfo, riderDetails, and quotes")] string bookingRequestJson,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bookingRequest = JsonSerializer.Deserialize<BookingRequest>(bookingRequestJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (bookingRequest == null)
            {
                return "Invalid booking request JSON provided.";
            }

            var response = await apiService.CreateBookingAsync(bookingRequest, cancellationToken);
            
            if (response == null || response.Length == 0)
            {
                return "Booking creation failed - no response received.";
            }

            return JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        catch (JsonException ex)
        {
            return $"Invalid JSON format: {ex.Message}";
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("402"))
        {
            return "Booking was declined by the system.";
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("500"))
        {
            return "Booking validation failed - please check the provided data.";
        }
        catch (HttpRequestException ex)
        {
            return $"API request failed: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error creating booking: {ex.Message}";
        }
    }

    [McpServerTool]
    [Description("Get information about supported trip types, car types, and price types for the Minicabit API.")]
    public static string GetApiInfo()
    {
        var apiInfo = new
        {
            tripTypes = new[]
            {
                new { name = "Single", description = "One-way trip from pickup to destination" },
                new { name = "Return", description = "Round trip with return time and date" },
                new { name = "Split", description = "Separate outbound and inbound bookings for larger time gaps" }
            },
            carTypes = new[]
            {
                "(4-seater)", "(-4-seater)", "(6-seater)", "(7-seater)", 
                "(8-seater)", "(9-seater)", "(14-seater)"
            },
            priceTypes = new[]
            {
                new { code = "PMP", description = "Pre-agreed pricing" },
                new { code = "LP", description = "List pricing" },
                new { code = "PAP", description = "Partner agreed pricing" }
            },
            dateTimeFormats = new
            {
                date = "YYYY-MM-DD (e.g., 2016-10-05)",
                time = "HH:MM (24-hour format, e.g., 17:30)"
            },
            authentication = "API key required in X-Minicabit-ApiKey-ID header",
            baseUrl = "https://api.minicabit.com/v1"
        };

        return JsonSerializer.Serialize(apiInfo, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    [McpServerTool]
    [Description("Generate a sample booking request JSON template with all required fields filled with example data.")]
    public static string GenerateBookingTemplate(
        [Description("Trip type: Single, Return, or Split")] string tripType = "Single",
        [Description("Number of passengers")] int passengers = 1)
    {
        var template = new BookingRequest
        {
            TripInfo = new TripInfo
            {
                PickUpDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                PickUpTime = "17:30",
                NumOfPassengers = passengers.ToString(),
                TripType = tripType,
                JourneyDurationMin = 30,
                Vias = Array.Empty<string>(),
                ReturnDate = tripType != "Single" ? DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") : null,
                ReturnTime = tripType != "Single" ? "19:30" : null,
                From = new LocationInfo
                {
                    FromProperty = "Elgin Rail Station",
                    FromPostCode = "IV30 1QP",
                    FromStreet = "Station Road",
                    FromTown = "Elgin",
                    FromCounty = "Moray",
                    Latitude = 57.649,
                    Longitude = -3.318
                },
                To = new LocationInfo
                {
                    ToProperty = "Dr Grays Hospital - Elgin",
                    ToPostCode = "IV30 1SN",
                    ToStreet = "Elgin Hospital",
                    ToTown = "Elgin",
                    ToCounty = "Moray",
                    Latitude = 57.648,
                    Longitude = -3.319
                }
            },
            RiderDetails = new RiderDetails
            {
                Id = 0,
                CustomerRegAccountId = 0,
                PassengerTitle = "Mr",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                MobileNum = "+447123456789",
                ReceiveSms = 1,
                ReceiveNewsletters = 0,
                AcceptedTAndCs = 1,
                SignUpNewsletter = 0
            },
            Quotes = new BookingQuotes
            {
                QuoteId = "sample-quote-id",
                FinalPrice = "25.00",
                Outbound = new OutboundQuote
                {
                    PhoId = "sample-operator-id",
                    Rating = 4.5,
                    StarRating = 4.5,
                    CarType = "(4-seater)",
                    Currency = "GBP",
                    FinalPrice = 25.00,
                    PriceType = "LP",
                    Name = "Sample Taxi Operator"
                }
            },
            UserAgent = "MinicabitMCP/1.0",
            OrderId = Guid.NewGuid().ToString()
        };

        return JsonSerializer.Serialize(template, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
} 