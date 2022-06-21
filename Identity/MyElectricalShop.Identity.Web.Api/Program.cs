using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyElectricalShop.Identity.Domain.Models;
using MyElectricalShop.Identity.Infrastructure.Data;
using MyElectricalShop.Identity.Web.Api;
using MyElectricalShop.Identity.Web.Api.ExtensionsForProgram;

var builder = WebApplication.CreateBuilder(args);

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Host
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

builder.Services.AddMvc();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatRCase();

builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddScoped<SignInManager<User>>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IUserStore<User>, UserStore<User, IdentityRole<Guid>, IdentityContext, Guid>>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerCase(builder.Configuration);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddEntityFrameworkNpgsql()
    .AddDbContext<IdentityContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityContext")));

builder.Services.AddIdentityServer(options =>
{
    options.UserInteraction.LoginUrl = "/login";
    options.UserInteraction.ErrorUrl = "/error";
})
    .AddAspNetIdentity<User>()
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources())
    .AddInMemoryApiResources(IdentityConfig.ApiResources())
    .AddInMemoryApiScopes(IdentityConfig.GetApiScopes(builder.Configuration))
    .AddInMemoryClients(IdentityConfig.GetClients(builder.Configuration))
    .AddDeveloperSigningCredential();

builder.Services.AddAuthenticationCase(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddMassTransit(x => x.UsingRabbitMq());



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "Identity");
        options.OAuthClientId("swagger");
        options.OAuthClientSecret("secret_swagger");
        options.OAuthAppName("Identity");
        options.OAuthUsePkce();
    });
}
app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseStaticFiles();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
