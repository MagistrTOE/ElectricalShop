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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyElectricalShop.Web.Api;
using Swashbuckle.AspNetCore.Filters;
using IdentityServer4;
using System.Security.Claims;

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

builder.Services.AddCors(options =>
{
    // задаём политику CORS, чтобы наше клиентское приложение могло отправить запрос на сервер API
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:10000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Add services to the container.
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IVoltageLevelRepository, VoltageLevelRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();



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
    options.SwaggerDoc("v1", new() { Title = "MyElectricalShop", Version = "v1", Description = "MyElectricalShop"});
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        In = ParameterLocation.Header,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:10000/connect/authorize"),
                TokenUrl = new Uri("http://localhost:10000/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"MyElectricalShop", "MyElectricalShop"}
                }
            }
        }
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "http://localhost:10000";
        options.Audience = "MyElectricalShop";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = new TimeSpan(1, 0, 0),
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

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

app.UseHttpsRedirection();


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
