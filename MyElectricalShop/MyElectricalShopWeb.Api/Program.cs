using AutoMapper;
using Core.Extension;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Infrastructure;
using MyElectricalShop.Infrastructure.Data;
using MyElectricalShop.Infrastructure.Data.Repositories;
using MyElectricalShop.Infrastructure.Repositories;
using System.Reflection;
using Serilog;
using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;

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
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IVoltageLevelRepository, VoltageLevelRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
//builder.Services.AddTransient<IValidator<CreateCompanyRequest>, CreateCompanyValidator>();



var assemblies = DependencyContext.Default.RuntimeLibraries
.SelectMany(assembly => assembly.GetDefaultAssemblyNames(DependencyContext.Default)
    .Where(assemblyName => assemblyName.FullName.StartsWith(nameof(MyElectricalShop)))
    .Select(Assembly.Load))
.ToArray();
builder.Services.AddMediatR(assemblies);



var mapper = new Mapper(new MapperConfiguration(ctx => ctx.AddMaps(assemblies)));


builder.Services.AddSingleton<IMapper>(mapper);

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(assemblies));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "MyElectricalShop", Version = "v1" });
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://localhost:10001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"SwaggerAPI", "MyAPI"}
                }
            }
        }
    });
});
    
    

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
})
    .AddIdentityServerAuthentication(options =>
    {
        options.ApiName = "SwaggerAPI";
        options.Authority = "https://localhost:10001";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<MyElectricalShopContext>();
await context.Database.MigrateAsync(CancellationToken.None);



// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyElectricalShop"));
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
