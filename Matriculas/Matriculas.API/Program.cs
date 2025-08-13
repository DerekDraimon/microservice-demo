using Matriculas.Application;
using Matriculas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().WriteTo.Seq("http://seq"));

builder.Services.AddDbContext<MatriculasDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddSingleton<IEventBus>(_ => new RabbitMqEventBus("rabbitmq"));
builder.Services.AddHostedService<CursoCreadoListener>();
builder.Services.AddScoped<MatricularEstudianteHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
