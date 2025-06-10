using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using MinicabitMcp.Services;

var builder = Host.CreateApplicationBuilder(args);

// Configure logging to stderr for MCP compatibility
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Add services
builder.Services.AddHttpClient<MinicabitApiService>();
builder.Services.AddScoped<MinicabitApiService>();

// Configure MCP server
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

// Build and run
var host = builder.Build();
await host.RunAsync();
