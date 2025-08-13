using Microsoft.EntityFrameworkCore;
using Pagos.Application;
using Pagos.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.Seq("http://seq"));

builder.Services.AddDbContext<PagosDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddSingleton<IEventBus>(_ => new RabbitMqEventBus("rabbitmq"));
builder.Services.AddScoped<ProcesarPagoHandler>();
builder.Services.AddHostedService<MatriculaIniciadaListener>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
