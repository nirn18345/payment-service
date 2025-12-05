using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentsService.Api.Middlewares;
using PaymentsService.Application.Interface;
using PaymentsService.Infraestructure.Data;
using PaymentsService.Infraestructure.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// DB CONTEXT
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// SERVICES
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IEventProducer, KafkaProducer>();


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// LOGGER
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// SUPPORT SERVICES
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API PaymentsService",
        Version = "v1",
        Description = "API para la gestión y procesamiento de pagos de servicios básicos.",

        Contact = new OpenApiContact
        {
            Name = "Nelson Rodriguez",
            Email = "nirn18345@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseGlobalErrorHandling();


app.MapControllers();

app.Run();
