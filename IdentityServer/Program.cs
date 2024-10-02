using IdentityServer.Extentions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add Serilog configuration
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug() // Set the log level
        .WriteTo.Console()    // Write logs to the console
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Write logs to a file
        .CreateLogger();

    builder.Host.UseSerilog();  // Use Serilog for logging

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    //builder.Services
    //    .InjectRazorPagesAndApi()
    //    .InjectUnitOfWork()
    //    .InjectIdp(builder.Configuration);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}