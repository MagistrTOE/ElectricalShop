using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyElectricalShop.Infrastructure.Data;
using MyElectricalShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Linq;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Infrastructure.Repositories;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var host = builder.Host
    .ConfigureHostConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();
    });


// Add services to the container.
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddTransient<IProductRepository, ProductRepository>();



var assemblies = DependencyContext.Default.RuntimeLibraries
.SelectMany(assembly => assembly.GetDefaultAssemblyNames(DependencyContext.Default)
    .Where(assemblyName => assemblyName.FullName.StartsWith(nameof(MyElectricalShop)))
    .Select(Assembly.Load))
.ToArray();
builder.Services.AddMediatR(assemblies);




var mapper = new Mapper(new MapperConfiguration(ctx =>
{
    var assemblies = DependencyContext.Default.RuntimeLibraries
        .SelectMany(assembly => assembly.GetDefaultAssemblyNames(DependencyContext.Default)
            .Where(assemblyName => assemblyName.FullName.StartsWith(nameof(MyElectricalShop)))
            .Select(Assembly.Load))
        .ToArray();

    ctx.AddMaps(assemblies);
}));

builder.Services.AddSingleton<IMapper>(mapper);



builder.Services.AddControllers();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MyElectricalShop", Version = "v1" });
});

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<MyElectricalShopContext>();
await context.Database.MigrateAsync(CancellationToken.None);



// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyElectricalShop v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
