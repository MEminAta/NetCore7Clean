using Application.PipelineBehaviors;
using Application.ServiceRegistrations;
using Application.ServiceRegistrations.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new AutofacBusinessModule(builder.Configuration));
    });

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


app.MapControllers();


app.Run();