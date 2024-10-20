using Application;
using dotenv.net;
using FluentValidation;
using Infrastructure.Config;
using Microsoft.AspNetCore.ResponseCompression;
using SerilogTracing;
using WebApi;
using WebApi.Hubs;

// set global fluent validation cascade mode to stop
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

// load .env
var solutionDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent;
DotEnv.Fluent()
    .WithTrimValues()
    .WithEnvFiles($"{solutionDir}/.env")
    .WithOverwriteExistingVars()
    .Load();

var builder = WebApplication.CreateBuilder(args);

// added this, if not needed remove
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});
builder.Host.UseConfiguredSerilog();

// for serilog - seq tracing
using var _ = new ActivityListenerConfiguration()
    .Instrument.AspNetCoreRequests(options => options.IncomingTraceParent = IncomingTraceParent.Trust)
    .Instrument.SqlClientCommands()
    .TraceToSharedLogger();

builder.WebHost.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
builder.WebHost.UseStaticWebAssets();

// service registration from configurations.
ConfigurationBase.ConfigureServicesFromAssemblies(builder.Services, [
    nameof(Domain), nameof(Application), nameof(Infrastructure),
    nameof(Persistence), nameof(WebApi),
]);

var app = builder.Build();

// added this, if not needed remove
app.UseCors();
app.UseResponseCompression();

app.UseConfiguredSerilogRequestLogging();
app.MapHub<QrLoginHub>("/qrlogin");
await app.MigrateDatabaseAsync();
app.UseApplicationMiddleware();

app.Run();