using System.Reflection;
using GameWatch.Backend.Behaviors;
using GameWatch.Backend.MappingProfiles;
using GameWatch.DataAccess;
using GameWatch.Infrastructure;
using GameWatch.Infrastructure.Abstractions;
using GameWatch.Infrastructure.Common;
using GameWatch.UseCases.Games.CreateGame;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "1.0",
            Title = "Game Watch API"
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddAutoMapper(typeof(GameMappingProfile));

    builder.Services.AddMediatR(typeof(CreateGameCommand));

    builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    builder.Services.AddTransient<IIgdbService, IgdbService>();

    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");

    throw;
}
finally
{
    LogManager.Shutdown();
}