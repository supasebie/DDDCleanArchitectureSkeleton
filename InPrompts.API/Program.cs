using System.Reflection;
using Ardalis.ListStartupServices;
using Ardalis.SharedKernel;
using FastEndpoints;
using FastEndpoints.Swagger;
using InPrompts.UseCases;
using InPrompts.Core;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;
using InPrompts.Infrastructure;
using FastEndpoints.ApiExplorer;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

logger.Information("Starting web host");
logger.Information("Environment: {environment}", builder.Environment.EnvironmentName);
logger.Information("Content root path: {contentRoot}", builder.Environment.ContentRootPath);
logger.Information("Web root path: {webRoot}", builder.Environment.WebRootPath);
logger.Information("Web root file provider: {provider}", builder.Environment.WebRootFileProvider);


builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));
var microsoftLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

// Configure Web Behavior
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddFastEndpoints();
builder.Services.AddFastEndpointsApiExplorer();
builder.Services.SwaggerDocument(o =>
                {
                    o.ShortSchemaNames = true;
                });

// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
//     c.EnableAnnotations();
//     string xmlCommentFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "swagger-docs.xml");
//     c.IncludeXmlComments(xmlCommentFilePath);
//     c.OperationFilter<FastEndpointsOperationFilter>();
// });

// builder.Services.AddCoreServices(microsoftLogger);
builder.Services.AddInfrastructureServices(builder.Configuration, microsoftLogger, builder.Environment.IsDevelopment());

ConfigureMediatR();

if (builder.Environment.IsDevelopment())
{
    // Use a local test email server
    // See: https://ardalis.com/configuring-a-local-test-email-server/
    // builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();

    // Otherwise use this:
    //builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
    AddShowAllServicesSupport();
}
else
{
    // builder.Services.AddScoped<IEmailSender, MimeKitEmailSender>();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
    app.UseDefaultExceptionHandler(); // from FastEndpoints
    app.UseHsts();
}

app.UseFastEndpoints()
    .UseSwaggerGen(); // Includes AddFileServer and static files middleware
                      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                      //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.UseHttpsRedirection();

await SeedDatabase(app);

app.Run();

static async Task SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        //          context.Database.Migrate();
        context.Database.EnsureCreated();
        await SeedData.InitializeAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

void ConfigureMediatR()
{
    var mediatRAssemblies = new[]
  {
  Assembly.GetAssembly(typeof(Prompt)), // Core
  Assembly.GetAssembly(typeof(CreatePromptCommand)) // UseCases
};
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
}

void AddShowAllServicesSupport()
{
    // add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
    builder.Services.Configure<ServiceConfig>(config =>
    {
        config.Services = new List<ServiceDescriptor>(builder.Services);

        // optional - default path to view services is /listallservices - recommended to choose your own path
        config.Path = "/listservices";
    });
}

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
