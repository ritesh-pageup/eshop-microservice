using BuildingBlock.Behaviors;
using BuildingBlock.Behaviours;
using BuildingBlock.CQRS;
using BuildingBlock.Exceptions.Handler;
using Catalog.API.Database;
using Catalog.API.Services.Products.CreateProduct;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Register FluentValidation and automatically scan for validators
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
string connectionString = mongoDbSettings["ConnectionString"];
string databaseName = mongoDbSettings["DatabaseName"];

//builder.Services.AddDbContext<CatalogDbContext>(options =>
//{
//    options.UseMongoDB(connectionString, databaseName);
//});


builder.Services.AddSingleton<IMongoClient>(prov =>
{
    // Retrieve connection string from configuration
    return new MongoClient(connectionString);
});
builder.Services.AddSingleton(prov =>
{
    var client = prov.GetRequiredService<IMongoClient>();
    // Retrieve database name from configuration
    return client.GetDatabase(databaseName);
});
builder.Services.AddDbContext<CatalogDbContext>((prov, options) =>
{
    var database = prov.GetRequiredService<IMongoDatabase>();
    options.UseMongoDB(
        database.Client,
        database.DatabaseNamespace.DatabaseName);
});

builder.Services.AddScoped<ICommandHandler<CreateProductCommand, CreateProductResult>, CreateProductCommandHandler>();

// Register Carter for endpoints
builder.Services.AddCarter();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddMongoDb(connectionString ?? "");


var app = builder.Build();



app.MapCarter();

//if (!app.Environment.IsDevelopment())
//{
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
//        if (exception == null)
//        {
//            return;
//        }

//        var problemDetail = new ProblemDetails
//        {
//            Title = exception.Message,
//            Detail = exception.StackTrace,
//            Status = StatusCodes.Status500InternalServerError
//        };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetail);
//    });
//});

//}
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
