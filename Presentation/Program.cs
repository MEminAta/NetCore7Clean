using System.ComponentModel;
using System.Diagnostics;
using Application;
using Application.Features.Roles.Rules;
using Application.PipelineBehaviors;
using Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TestPipeline<,>));


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure the HTTP request pipeline.
var app = builder.Build();
Console.WriteLine();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

var watch = new Stopwatch();
app.Use(async (context, next) =>
{
    watch.Start();
    await next(context);
    Console.WriteLine(watch.Elapsed.TotalSeconds);
    watch.Reset();
});


var entity = new MyEntity { MyProperty = false };
entity.MyProperty = true;


app.Run();