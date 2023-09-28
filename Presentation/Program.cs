using System.Diagnostics;
using Application;
using CrossCuttingConcern.AutoLog;
using CrossCuttingConcern.Globalization;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


if (app.Environment.IsProduction())
{
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<GlobalizationMiddleware>();
app.UseMiddleware<LogMiddleware>();

app.MapControllers();

var watch = new Stopwatch();
app.Use(async (context, next) =>
{
    var breakActive = builder.Configuration.GetValue<bool>("MaintenanceBreak");
    if (breakActive) return;

    watch.Start();
    await next(context);
    Console.WriteLine(watch.Elapsed.TotalSeconds);
    watch.Reset();
});

app.Run();