# Minicabit MCP Server

A Model Context Protocol (MCP) server that provides tools for interacting with the Minicabit Partner API. This server exposes Minicabit's taxi booking platform functionality as MCP tools that can be used with AI assistants and other MCP clients.

## Features

The MCP server provides the following tools based on the Minicabit Partner API:

### 🚗 **GetQuotes**
Search for taxi quotes between two locations with support for:
- Single trips (one-way)
- Return trips (round-trip)
- Split trips (separate bookings with time gaps)
- Multiple passengers
- Via points/stops
- Luggage specifications

### 🛣️ **CheckRoute** 
Compare route options for trips with multiple stops to find cheaper alternatives.

### 📋 **CreateBooking**
Create actual taxi bookings in the Minicabit system with complete trip and passenger details.

### ℹ️ **GetApiInfo**
Get information about supported trip types, car types, pricing models, and API specifications.

### 📝 **GenerateBookingTemplate**
Generate sample booking request templates with all required fields and example data.

## Setup

### Prerequisites
- .NET 9.0 or later
- Minicabit Partner API key

### Installation

1. **Clone and build the project:**
   ```bash
   git clone <repository-url>
   cd minicabit-mcp
   dotnet build
   ```

2. **Configure your API key:**
   
   Set your Minicabit API key in one of the following ways:
   
   **Option A: Environment Variable**
   ```bash
   export Minicabit__ApiKey="your-api-key-here"
   ```
   
   **Option B: appsettings.json**
   ```json
   {
     "Minicabit": {
       "ApiKey": "your-api-key-here"
     }
   }
   ```
   
   **Option C: Command line**
   ```bash
   dotnet run --Minicabit:ApiKey="your-api-key-here"
   ```

3. **Run the MCP server:**
   ```bash
   dotnet run --project MinicabitMcp
   ```

## Usage Examples

### Getting Quotes

```json
{
  "tool": "GetQuotes",
  "parameters": {
    "fromProperty": "Elgin Rail Station (IV30 1QP)",
    "toProperty": "Dr Grays Hospital - Elgin(IV30 1SN)",
    "pickUpDate": "2024-12-15",
    "pickUpTime": "17:30",
    "numOfPassengers": 2,
    "tripType": "Single"
  }
}
```

### Return Trip Example

```json
{
  "tool": "GetQuotes",
  "parameters": {
    "fromProperty": "London Heathrow Airport (TW6 1AP)",
    "toProperty": "Central London Hotel (W1K 4HR)",
    "pickUpDate": "2024-12-15", 
    "pickUpTime": "14:00",
    "numOfPassengers": 1,
    "tripType": "Return",
    "returnDate": "2024-12-17",
    "returnTime": "10:00"
  }
}
```

### Route Comparison

```json
{
  "tool": "CheckRoute",
  "parameters": {
    "fromProperty": "Edinburgh Airport (EH12 9DN)",
    "toProperty": "Glasgow City Centre (G1 1AA)",
    "vias": "[{\"no\":\"1\",\"property\":\"Linlithgow\",\"postCode\":\"EH49 6QH\",\"latitude\":\"55.9772\",\"longitude\":\"-3.6032\"}]"
  }
}
```

### Creating a Booking

First, generate a template:
```json
{
  "tool": "GenerateBookingTemplate",
  "parameters": {
    "tripType": "Single",
    "passengers": 2
  }
}
```

Then use the template to create a booking:
```json
{
  "tool": "CreateBooking", 
  "parameters": {
    "bookingRequestJson": "{...complete booking JSON...}"
  }
}
```

## API Reference

### Supported Trip Types
- **Single**: One-way trip
- **Return**: Round trip with return time
- **Split**: Separate outbound and inbound bookings

### Supported Car Types
- (4-seater)
- (-4-seater)  
- (6-seater)
- (7-seater)
- (8-seater)
- (9-seater)
- (14-seater)

### Price Types
- **PMP**: Pre-agreed pricing
- **LP**: List pricing  
- **PAP**: Partner agreed pricing

### Date/Time Formats
- **Dates**: YYYY-MM-DD (e.g., "2024-12-15")
- **Times**: HH:MM in 24-hour format (e.g., "17:30")

## Integration with MCP Clients

This server works with any MCP-compatible client. Here are some popular options:

### Claude Desktop
Add to your Claude Desktop configuration:
```json
{
  "mcpServers": {
    "minicabit": {
      "command": "dotnet",
      "args": ["run", "--project", "/path/to/MinicabitMcp"],
      "env": {
        "Minicabit__ApiKey": "your-api-key-here"
      }
    }
  }
}
```

### Cline/Continue
Configure as an MCP server in your IDE extension settings.

### Custom Integration
Use the stdio transport to integrate with any MCP client:
```bash
dotnet run --project MinicabitMcp
```

## Error Handling

The server provides detailed error messages for common scenarios:
- **401 Unauthorized**: Invalid or missing API key
- **402 Payment Required**: Booking declined by system  
- **500 Internal Server Error**: Validation errors
- **Network errors**: Connection issues with Minicabit API

## Security

- API keys are handled securely through configuration
- All requests use HTTPS to the Minicabit API
- Input validation prevents malformed requests
- Error messages don't expose sensitive information

## Development

### Project Structure
```
MinicabitMcp/
├── Models/           # API request/response models
├── Services/         # HTTP client service for Minicabit API
├── Tools/           # MCP tool implementations
├── Program.cs       # MCP server setup
└── appsettings.json # Configuration
```

### Dependencies
- **ModelContextProtocol** - MCP .NET SDK
- **Microsoft.Extensions.Hosting** - .NET hosting framework
- **System.Text.Json** - JSON serialization

## License

[Add your license information here]

## Support

For Minicabit API questions, visit: https://api.minicabit.com/v1/partner-docs/

For MCP Server issues, please create an issue in this repository. 