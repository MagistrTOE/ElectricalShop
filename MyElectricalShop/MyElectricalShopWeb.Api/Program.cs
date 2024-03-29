using System.Reflection;
using AutoMapper;
using Core.Extension;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using MyElectricalShop.Application.RabbitMQ;
using MyElectricalShop.Infrastructure;
using MyElectricalShop.Infrastructure.Data;
using MyElectricalShop.Web.Api.ExtensionsForProgram;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var host = builder.Host
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureHostConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
        .AddEnvironmentVariables();
    });

// Add services to the container.

builder.Services.AddInfrastructureService(builder.Configuration);

var assemblies = DependencyContext.Default.RuntimeLibraries
    .SelectMany(assembly => assembly.GetDefaultAssemblyNames(DependencyContext.Default)
    .Where(assemblyName => assemblyName.FullName.StartsWith(nameof(MyElectricalShop)))
    .Select(Assembly.Load))
    .ToArray();

builder.Services.AddMediatR(assemblies);
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(assemblies));

var mapper = new Mapper(new MapperConfiguration(ctx => ctx.AddMaps(assemblies)));
builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddSwaggerCase(builder.Configuration);

builder.Services.AddAuthenticationCase(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

builder.Services.AddControllers(options =>
{
    options.UseCentralRoutePrefix("shop");
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer(typeof(CreateCartConsumer));
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.ReceiveEndpoint("MyQueue", x =>
        {
            x.ConfigureConsumers(context);
        });
        cfg.Host("rabbitmq", x =>
        {
            x.Username("guest");
            x.Password("guest");
        });
    });
});

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<MyElectricalShopContext>();
await context.Database.MigrateAsync(CancellationToken.None);

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionMiddleware();

//app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "MyElectricalShop");
    options.OAuthClientId("swagger_shop");
    options.OAuthClientSecret("secret_swagger_shop");
    options.OAuthAppName("MyElectricalShop");
    options.OAuthUsePkce();
});
app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
