using CatalogoCursos.Application;
using CatalogoCursos.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration)
       .WriteTo.Seq(ctx.Configuration["Seq:ServerUrl"] ?? "http://seq:5341"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogoCursosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogoCursosDb")));

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddSingleton<IEventBus>(_ =>
    new RabbitMqEventBus(builder.Configuration.GetConnectionString("RabbitMq") ?? "rabbitmq"));
builder.Services.AddScoped<CrearCursoHandler>();

builder.Services.AddOpenTelemetry()
    .WithTracing(b => b
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddJaegerExporter())
    .WithMetrics(b => b
        .AddPrometheusExporter());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapPrometheusScrapingEndpoint();

app.Run();
