using Application;
using AspNetCoreRateLimit;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Middleware;
using Presentation.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIpRateLimiting(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientAppPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

    options.AddPolicy("HostPolicy",
        builder =>
        {
            builder.WithOrigins("https://swift-list-mu.vercel.app")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("ClientAppPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseIpRateLimiting();

app.MapControllers();

app.UseCoreContext();

app.Run();
