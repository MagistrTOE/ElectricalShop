using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Infrastructure;
using MyElectricalShop.Infrastructure.Data;
using MyElectricalShop.Infrastructure.Data.Repositories;
using MyElectricalShop.Infrastructure.Repositories;
using System.Reflection;

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
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IVoltageLevelRepository, VoltageLevelRepository>();



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
