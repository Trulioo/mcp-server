using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Trulioo.MCPServerForKYB.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile(AppContext.BaseDirectory + "appsettings.json");

#if DEBUG
builder.Configuration.AddUserSecrets<Program>();
#endif

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);
builder.Services.AddSingleton<TruliooClientOptions>(builder.Configuration.GetSection("TruliooClient")
    .Get<TruliooClientOptions>());
builder.Logging.AddConsole(o =>
{
    o.LogToStandardErrorThreshold = LogLevel.Trace;
});
builder.Services.AddSingleton<TruliooClient>();
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithPromptsFromAssembly()
    .WithResourcesFromAssembly()
    .WithToolsFromAssembly();
var host = builder.Build();
// Run the application
await host.RunAsync();

